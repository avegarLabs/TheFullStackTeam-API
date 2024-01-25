using System.Linq.Expressions;
using TheFullStackTeam.Application.Model.Base;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.GET;

public class ClientLookup : NickNamedModel
{
    public string Name { get; set; } = null!;

    public static Expression<Func<Client, ClientLookup>> Projection =>
        x => new ClientLookup
        {
            Moniker = x.Moniker,
            Name = x.Name,
        };
}