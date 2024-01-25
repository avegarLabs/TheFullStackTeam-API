using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities
{
    public class Invoice : NicknamedEntity
    {
        public string Number { get; set; }
        public string Emitter { get; set; }
        public string Reciever { get; set; }
        public DateTime IssueDates { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Amount { get; set; }
        public string PayDetail { get; set; } = null!;
        public string State { get; set; }

        public virtual Timesheet Timesheet { get; set; }

        public Guid ProfessionalId { get; set; }
        public virtual Professional Professional { get; set; }




    }
}
