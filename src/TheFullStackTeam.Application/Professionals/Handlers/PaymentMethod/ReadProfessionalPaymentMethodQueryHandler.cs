using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Professionals.Queries;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.PaymentMethod
{
    public class ReadProfessionalPaymentMethodQueryHandler : AppRequestHandler, IRequestHandler<ReadProfessionalPaymentMethodsQuery, ReadPaymentMethodResult>
    {
        public ReadProfessionalPaymentMethodQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ReadPaymentMethodResult> Handle(ReadProfessionalPaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            var response = _context.PaymentMethods.Where(pm => pm.ProfessionalId == request.ProfessinalId).AsNoTracking().Select(PaymentMethodListItem.Projection).ToList();
            return new ReadPaymentMethodResult(response);
        }
    }
}
