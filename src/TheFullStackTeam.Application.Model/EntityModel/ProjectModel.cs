using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class ProjectModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Guid ClientId { get; set; }
        public DateTime? DueDate { get; set; }

        public static implicit operator Project(ProjectModel model)
            => new()
            {
                Name = model.Name,
                Description = model.Description,
                ClientId = model.ClientId,
                DueDate = model.DueDate

            };
    }
}
