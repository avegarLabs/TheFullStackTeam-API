using MediatR;
using TheFullStackTeam.Application.Professionals.Results;

namespace TheFullStackTeam.Application.Professionals.Queries
{
    public class GetProfessionalCvPdfQuery:IRequest<GetProfessionalCvPDFResults>
    {
        public Guid ProfessionalId { get; set; }
        public GetProfessionalCvPdfQuery(Guid profId)
        {
            ProfessionalId = profId;
        }
    }
}
