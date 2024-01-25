using Microsoft.AspNetCore.Authorization;
namespace TheFullStackTeam.API.AuthPolicies
{
    /// <summary>
    /// Is Admin Requirement
    /// </summary>
    /// <seealso cref="IAuthorizationRequirement" />
    public class IsAdminRequirement: IAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsAdminRequirement"/> class.
        /// </summary>
        public IsAdminRequirement()
        {

        }
    }
}
