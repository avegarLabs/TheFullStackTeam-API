using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class OrganizationServiceModel
    {
        public string ServicesName { get; set; } = null!;
        public double Price { get; set; }
        public string Currency { get; set; } = null!;
        public string ServiceDescription { get; set; }

        public IEnumerable<string> SkillList { get; set; }

        public List<string> CategoryList { get; set; }

        public static implicit operator OrganizationSevices(OrganizationServiceModel model) => new()
        {
            ServiceName = model.ServicesName,
            ServiceDescription = model.ServiceDescription,
            SevicePrice = model.Price,
            Currency = model.Currency,
        };
    }
}
