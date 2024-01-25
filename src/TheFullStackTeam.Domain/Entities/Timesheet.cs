using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities
{
    public class Timesheet : BaseEntity
    {
        public string Month { get; set; }

        public string State { get; set; }

        public Guid InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

    }
}
