using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using Hangfire.Dashboard;

namespace Infrastructure.BackgroundJobs.Hangfire
{
    public class BasicDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            //var httpContext = context.GetHttpContext();
            //if (!httpContext.User.Identity.IsAuthenticated)
            //    return false;

            //var user = httpContext.User;

            //return user.Claims.Any(c =>
            //    (c.Type == "role" || c.Type.EndsWith("/role"))
            //    && c.Value == "ADMIN");

            return true;
        }
    }
}
