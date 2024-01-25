using Microsoft.Graph;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Services.Abstract;

namespace TheFullStackTeam.Application.Services.Contracts
{
    public interface IUserService:IService
    {
        Task<User> CreateUser(UserModel user);

        Task<IGraphServiceUsersCollectionPage> GetUserByEmail(string email);

        Task<bool> DeleteUser(string email);

    }
}
