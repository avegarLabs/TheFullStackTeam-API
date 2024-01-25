using MediatR;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Professionals.Queries
{
    public class ReadProfessionalPaymentMethodsQuery : IRequest<ReadPaymentMethodResult>
    {

        public Guid ProfessinalId { get; set; }
        public ReadProfessionalPaymentMethodsQuery(Guid id)
        {
            ProfessinalId = id;
        }
    }
}
