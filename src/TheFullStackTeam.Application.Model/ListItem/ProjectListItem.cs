using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class ProjectListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string ClientName { get; set; } = null!;
        public Guid ClientId { get; set; }
        public DateTime? DueDate { get; set; }

        public static implicit operator ProjectListItem(Project domainEntity)
            => new()
            {
                Id = domainEntity.Id,
                Name = domainEntity.Name,
                Description = domainEntity.Description,
                ClientId = domainEntity.Client.Id,
                DueDate = domainEntity.DueDate
            };

        public static Expression<Func<Project, ProjectListItem>> Projection =>
            x => new ProjectListItem
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ClientName = x.Client!.Name,
                ClientId = x.Client.Id,
                DueDate = x.DueDate
            };
    }
}
