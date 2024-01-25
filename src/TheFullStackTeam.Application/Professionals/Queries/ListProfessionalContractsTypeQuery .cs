using MediatR;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalContractType;

namespace TheFullStackTeam.Application.Professionals.Queries;

/// <summary>
/// A professional skills list query 
/// </summary>
public class ListProfessionalContractsTypeQuery : IRequest<ListProfessionalContractTypeCommandResult>
{
    public Guid ProfessionalId { get; set; }

    public ListProfessionalContractsTypeQuery(Guid id)
    {
        ProfessionalId = id;
    }


}

