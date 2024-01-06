using Microsoft.AspNetCore.Authorization;

namespace identityproduct_app.Identity.PolicyRequirements
{
    public class BusinessHoursRequirement : IAuthorizationRequirement
    {
        public BusinessHoursRequirement()
        {
            
        }
    }
}
