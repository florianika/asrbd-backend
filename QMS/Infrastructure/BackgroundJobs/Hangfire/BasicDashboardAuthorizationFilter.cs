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

            //// Ensure user is authenticated
            //if (!httpContext.User.Identity?.IsAuthenticated ?? false)
            //    return false;

            //if(!httpContext.User.IsInRole("ADMIN"))
            //{
            //    // Allow access for Admin users
            //    return false;
            //}            
            return true;
        }
    }
}
