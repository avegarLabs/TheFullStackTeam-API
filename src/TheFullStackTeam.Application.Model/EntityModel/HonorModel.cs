using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class HonorModel
    {
        public string Title { get; set; } = null!;
        public string OrganizationName { get; set; } = null!;
        public Guid? OrganizationId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime IssueDate { get; set; }

        public static implicit operator Honor(HonorModel model) => new()
        {
            Title = model.Title,
            OrganizationName = model.OrganizationName,
            Description = model.Description,
            IssueDate = model.IssueDate,
            OrganizationId = model.OrganizationId,
        };
    }
}
