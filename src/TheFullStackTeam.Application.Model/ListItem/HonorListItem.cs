using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class HonorListItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string OrganizationName { get; set; } = null!;
        public Guid? OrganizationId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime IssueDate { get; set; }
        public Guid ProfessionalId { get; set; }

        public static implicit operator HonorListItem(Honor domainEntity) => new()
        {
            Id = domainEntity.Id,
            Title = domainEntity.Title,
            Description = domainEntity.Description,
            ProfessionalId = domainEntity.ProfessionalId,
            IssueDate = domainEntity.IssueDate,
            OrganizationName = domainEntity.OrganizationName,
            OrganizationId = domainEntity.OrganizationId,
        };

        public static Expression<Func<Honor, HonorListItem>> Projection =>
            x => new HonorListItem
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ProfessionalId = x.ProfessionalId,
                IssueDate = x.IssueDate,
                OrganizationName = x.OrganizationName,
                OrganizationId = x.OrganizationId,
            };
    }
}
