using Microsoft.AspNetCore.Authorization;

namespace TheFullStackTeam.API.AuthPolicies
{
    /// <summary>
    /// Is Admin or Customer ServiceRequirement
    /// </summary>
    /// <seealso cref="IAuthorizationRequirement" />
    public class IsAdminOrCustomerServiceRequirement: IAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsAdminOrCustomerServiceRequirement"/> class.
        /// </summary>
        public IsAdminOrCustomerServiceRequirement() { }
    }
}
