using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class ProfessionalJobTypeModel
    {
        public string Name { get; set; }

        public static implicit operator ProfessionalJobType(ProfessionalJobTypeModel model) => new()
        {
            Name = model.Name,

        };

    }
}
