using MediatR;
using TheFullStackTeam.Application.Jobs.Results;

namespace TheFullStackTeam.Application.Jobs.Command
{
    public class UpdateJobAviabilityStateCommand:IRequest<CreateJobCommandResult> 
    {
        public Guid JobId { get; set; }
        public bool State { get; set; }

        public UpdateJobAviabilityStateCommand(Guid jobId, bool state)
        {
            JobId = jobId;
            State = state;
        }
    }
}
