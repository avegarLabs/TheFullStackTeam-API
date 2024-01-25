using MediatR;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Users.Queries
{
    public class ReadUserContactInformationQuery : IRequest<UserContactInformationResult>
    {
        public string AccountId { get; set; }

        public ReadUserContactInformationQuery(string accountId)
        {
            AccountId = accountId;
        }
    }
}

