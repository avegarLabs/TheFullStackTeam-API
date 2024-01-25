using Azure.Core;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Users.Command;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;
using WindowsAzure.Table.Extensions;

namespace TheFullStackTeam.Application.Users.Handler
{
    public class UpdateUserProfileCommandHandler : AppRequestHandler, IRequestHandler<UpdateUserProfileCommand, ReadUserProfileInformationResult>
    {
        public UpdateUserProfileCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ReadUserProfileInformationResult> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users.Where(u => u.Id == request.Model.Id).SingleOrDefault();
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Model.Name);
            }

            var roles = _context.UserRole.Where(ur => ur.UserId == user.Id).ToList();
            _context.UserRole.RemoveRange(roles);
            await _context.SaveChangesAsync(cancellationToken);
            var updateRoles = await ResetRolesInUser(user, request.Model.Roles);
                      
            user.Name = request.Model.Name;
            user.FirstName = request.Model.FirstName;
            user.LastName = request.Model.LastName;
            user.Address = new Domain.ValueObjects.Address(request.Model.City ?? string.Empty, request.Model.Country.CommonName ?? string.Empty, request.Model.Line1 ?? string.Empty, "", "", request.Model.OtherAddressDetails ?? string.Empty, request.Model.StateProvinceCountry ?? string.Empty, request.Model.ZipOrPostalCode ?? string.Empty);
            user.Phone = request.Model.Phone;
            user.ContactEmail = request.Model.ContactEmail;
            user.CountryId = request.Model.Country.Id;
            user.UserRoles = updateRoles;
            _context.Users.Update(user);
        
            await _context.SaveChangesAsync(cancellationToken);
            return new ReadUserProfileInformationResult(user);
        }

        private async Task<List<Domain.Entities.UserRoles>> ResetRolesInUser(User user, List<RolesUserListItem> roles)
        {
            List<Domain.Entities.UserRoles> userRoles = new List<Domain.Entities.UserRoles>();
            foreach(var item in roles)
            {
                var role = new Domain.Entities.UserRoles()
                {
                    RoleName = item.Name,
                    RoleId = item.Id,
                    UserId = user.Id
                };
                userRoles.Add(role);
            }
           return userRoles;
        }
    }
}
