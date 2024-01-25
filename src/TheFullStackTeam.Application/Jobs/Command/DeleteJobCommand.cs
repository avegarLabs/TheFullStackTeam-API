using MediatR;
using TheFullStackTeam.Application.Jobs.Results;

namespace TheFullStackTeam.Application.Jobs.Command
{
    public class DeleteJobCommand : IRequest<DeleteJobCommandResult>
    {
         public Guid JobId { get; }

        public DeleteJobCommand(Guid idJob)
        {
           JobId = idJob;
        }
    }
}
