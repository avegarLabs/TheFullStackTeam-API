using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TheFullStackTeam.RolesMemoryCache;

namespace TheFullStackTeam.API.AuthPolicies
{
    public class IsAdminOrCustomerServiceHandler : AuthorizationHandler<IsAdminOrCustomerServiceRequirement>
    {
        private readonly IRolesMemoryCache _rolesCache;

        public IsAdminOrCustomerServiceHandler(IRolesMemoryCache cacheService)
        {
          _rolesCache = cacheService;
        }

        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminOrCustomerServiceRequirement requirement)
        {
            var canAccess = false;
            var accountId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var roles = await _rolesCache.GetUserRoles(accountId);
            if (roles.Contains("ADMIN") || roles.Contains("CUSTOMERSERVICE"))
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
