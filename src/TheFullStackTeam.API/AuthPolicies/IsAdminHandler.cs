using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TheFullStackTeam.RolesMemoryCache;

namespace TheFullStackTeam.API.AuthPolicies
{
    /// <summary>
    /// Is Admin Policy Handler
    /// </summary>
    /// <seealso cref="AuthorizationHandler{IsAdminRequirement}" />
    public class IsAdminHandler: AuthorizationHandler<IsAdminRequirement>
    {

        private readonly IRolesMemoryCache _rolesCache;
       

        public IsAdminHandler(IRolesMemoryCache rolesCache)
        {
            _rolesCache = rolesCache;
        }

        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context,
           IsAdminRequirement requirement)
        {
            var canAccess = false;
            var accountId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var roles = await _rolesCache.GetUserRoles(accountId);
            if(roles.Contains("ADMIN"))
            {
                canAccess = true;
            }

            if (accountId != null && canAccess)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
