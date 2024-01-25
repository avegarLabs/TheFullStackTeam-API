using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Users.Command;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Users.Handler
{
    public class AddUserContactInformationCommandHandler : AppRequestHandler, IRequestHandler<AddUserContactInformationCommand, UserContactInformationResult>
    {
        public AddUserContactInformationCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<UserContactInformationResult> Handle(AddUserContactInformationCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(u => u.AccountId.Equals(request.AccountId)).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.AccountId);
            }
            user.Name = request.ContactInformation.Name;
            user.Phone = request.ContactInformation.Phone;
            user.ContactEmail = request.ContactInformation.ContactEmail;
            user.Address = new Domain.ValueObjects.Address(request.ContactInformation.City ?? string.Empty, request.ContactInformation.Country ?? string.Empty, request.ContactInformation.Line1 ?? string.Empty, request.ContactInformation.Line2 ?? string.Empty, request.ContactInformation.Line3 ?? string.Empty, request.ContactInformation.OtherAddressDetails ?? string.Empty, request.ContactInformation.StateProvinceCountry ?? string.Empty, request.ContactInformation.ZipOrPostalCode ?? string.Empty);
            _context.Users.Update(user);

            await _context.SaveChangesAsync(cancellationToken);
            return new UserContactInformationResult(user);

        }
    }
}
