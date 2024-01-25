using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.PaymentMethod;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.PaymentMethod
{
    public class AddProfessionalPaymentMethodCommandHandler : AppRequestHandler, IRequestHandler<AddProfessionalPaymentMethodCommand, PaymentMethodResult>
    {
        public AddProfessionalPaymentMethodCommandHandler(TheFullStackTeamDbContext context) : base(context)
        { }

        public async Task<PaymentMethodResult> Handle(AddProfessionalPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Professionals.Where(u => u.Id == request.ProfessionalId).SingleOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(Professional), request.ProfessionalId);
            }
            Domain.Entities.PaymentMethod paymentMethod = request.Model;
            user.PaymentMethods.Add(paymentMethod);
            _context.Professionals.Update(user);
            await _context.SaveChangesAsync();

            return new PaymentMethodResult(paymentMethod);
        }


    }
}
