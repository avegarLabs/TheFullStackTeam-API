using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Application.Services.Abstract;

public interface IMonikerService : IService
{
    Task<string> FindValidMoniker<TEntity>(string suggestedMoniker) where TEntity : NicknamedEntity;
}