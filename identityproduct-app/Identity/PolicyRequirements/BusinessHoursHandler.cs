using Microsoft.AspNetCore.Authorization;

namespace identityproduct_app.Identity.PolicyRequirements
{
    public class BusinessHoursHandler : AuthorizationHandler<BusinessHoursRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BusinessHoursRequirement requirement)
        {
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);
            if(currentTime.Hour >= 8 && currentTime.Hour <= 18)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
