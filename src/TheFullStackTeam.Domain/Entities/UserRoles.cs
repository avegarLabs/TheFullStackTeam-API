using TheFullStackTeam.Domain.Entities.Base;

/// <summary>
/// User roles entity
/// </summary>
namespace TheFullStackTeam.Domain.Entities
{
    public class UserRoles : BaseEntity
    {
        public string RoleName { get; set; } = null!;

        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public Guid? RoleId { get; set; }
        public virtual Roles Roles { get; set; }
    }
}