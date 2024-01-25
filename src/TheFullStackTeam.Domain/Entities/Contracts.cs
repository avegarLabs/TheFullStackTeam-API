using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities
{
    public class Contracts : NicknamedEntity
    {
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public Guid ProfessionalId { get; set; }
        public virtual Professional Professional { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double SalaryRate { get; set; }

        public int FreeDays { get; set; }

        public string ContractsDetails { get; set; }


    }
}
