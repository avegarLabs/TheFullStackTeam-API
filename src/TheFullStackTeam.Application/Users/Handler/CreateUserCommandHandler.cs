using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Application.Services.Contracts;
using TheFullStackTeam.Application.Users.Command;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Domain.ValueObjects;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Users.Handler
{
    public class CreateUserCommandHandler : AppRequestHandler, IRequestHandler<CreateUserCommand, ReadUserProfileInformationResult>
    {

        private readonly IMonikerService _monikerService;
        private readonly IUserService _userService;
        private readonly IStorageService _storageService;
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="monikerService">The moniker service.</param>
        /// /// <param name="storageService">The storage service.</param>
        public CreateUserCommandHandler(TheFullStackTeamDbContext context, IUserService userService, IMonikerService monikerService, IStorageService storageService) : base(context)
        {
            _userService = userService;
            _monikerService = monikerService;
            _storageService = storageService;
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public async Task<ReadUserProfileInformationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.CreateUser(request.UserModel);
            Domain.Entities.User entity = request.UserModel;
            if (user != null)
            {
                try
                {
                    var country = _context.Countries.Where(u => u.CommonName.ToLower().Equals(request.UserModel.Country.ToLower())).SingleOrDefault();
                    entity.CountryId = country.Id;
                    
                    entity.AccountId = user.Id;
                    entity.Moniker = await _monikerService.FindValidMoniker<Domain.Entities.User>(entity.Name);
                    entity.ContactEmail = request.UserModel.ContactEmail;
                    entity.Phone= request.UserModel.Phone;
                    entity.Address = new Address(request.UserModel.City ?? string.Empty, country.CommonName ?? string.Empty, request.UserModel.Line1 ?? string.Empty, "", "", request.UserModel.OtherAddressDetails ?? string.Empty, request.UserModel.StateProvinceCountry ?? string.Empty, request.UserModel.ZipOrPostalCode ?? string.Empty);
                    await _context.Users.AddAsync(entity);
                    await _storageService.CreateDirectory(entity.Moniker);
                    await SetRolesToUser(entity, request.UserModel.Roles, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new ReadUserProfileInformationResult(entity);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {
                return new ReadUserProfileInformationResult(entity);
            }
        }
        private async Task SetRolesToUser(Domain.Entities.User user, List<string> roles, CancellationToken cancellationToken)
        {
            List<Domain.Entities.UserRoles> userRoles = new List<Domain.Entities.UserRoles>();
            foreach (var item in roles)
            {
                var role = _context.Roles.Where(r => r.Moniker.Equals(item)).AsNoTracking().SingleOrDefault();
                if (role != null)
                {
                    var userRole = new Domain.Entities.UserRoles()
                    {
                        RoleName = role.Name,
                        UserId = user.Id,
                        RoleId = role.Id
                    };
                    userRoles.Add(userRole);
                }
            }
            await _context.AddRangeAsync(userRoles);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
