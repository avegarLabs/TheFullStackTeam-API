using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class ProfessionalContractTypeModel
    {
        public string Name { get; set; }

        public static implicit operator ProfessionalContractType(ProfessionalContractTypeModel model) => new()
        {
            Name = model.Name

        };

    }
}
