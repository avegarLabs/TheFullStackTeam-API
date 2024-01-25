using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Application.Users.Command;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Communications.EmailTemplates.ViewModels;
using TheFullStackTeam.Communications.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Domain.ValueObjects;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Users.Handler
{
    public class RegisterUserCommandHandler : AppRequestHandler, IRequestHandler<RegisterUserCommand, ReadUserProfileInformationResult>
    {

        private readonly IMonikerService _monikerService;
        private readonly IStorageService _storageService;
        private readonly IEmailService _emailService;
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="monikerService">The moniker service.</param>
        /// /// <param name="storageService">The storage service.</param>
        public RegisterUserCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService, IStorageService storageService, IEmailService emailService) : base(context)
        {
            _monikerService = monikerService;
            _storageService = storageService;
            _emailService = emailService;
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public async Task<ReadUserProfileInformationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            var country = await _context.Countries.Where(c => c.CommonName.ToLower().Equals(request.UserModel.Country.ToLower())).FirstOrDefaultAsync(cancellationToken);
            Domain.Entities.User entity = request.UserModel;
            entity.AccountId = request.UserModel.AccountId;
            entity.Moniker = await _monikerService.FindValidMoniker<Domain.Entities.User>(request.UserModel.Name);
            entity.ContactEmail = request.UserModel.ContactEmail;
            entity.Phone = request.UserModel.Phone;
            entity.CountryId = country.Id;
            entity.Address = new Address(request.UserModel.City ?? string.Empty, country.CommonName ?? string.Empty, request.UserModel.Line1 ?? string.Empty, "", "", request.UserModel.OtherAddressDetails ?? string.Empty, request.UserModel.StateProvinceCountry ?? string.Empty, request.UserModel.ZipOrPostalCode ?? string.Empty);
            await _context.Users.AddAsync(entity);
            await _storageService.CreateDirectory(entity.Moniker);
            await SetRolesToUser(entity, request.UserModel.Roles, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await sentWelcomeEmail(entity);
            return new ReadUserProfileInformationResult(entity);

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

        private async Task sentWelcomeEmail(Domain.Entities.User? user)
        {
            var dataModel = new ProfessionalCreatedNotificationViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DisplayName = user.Name,
                Email = user.ContactEmail,
                Moniker = user.Moniker,
                Moto = user.Country.CommonName
            };
            await _emailService.SendProfessionalWelcomeEmail(dataModel, user.ContactEmail);
        }
    }
}
