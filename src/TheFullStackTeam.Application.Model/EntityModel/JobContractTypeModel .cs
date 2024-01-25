using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel;

public class JobContractTypeModel
{
    public string ContractTypeName { get; set; }
  
    public static implicit operator JobContractType(JobContractTypeModel model) => new()
    {
        ContractTypeName = model.ContractTypeName,
    };
}