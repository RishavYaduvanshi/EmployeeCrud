using EmployeeDetails.DB;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EmployeeDetails.BasicAuth
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null) 
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.Unauthorized, "Login Failed");
            }
            else 
            {
                var credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authHeader.Parameter));
                var username = credentials.Split(':')[0];
                var password = credentials.Split(':')[1];

                var isValid = username == "andy" && password == "pass";
                
                if (isValid)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username),null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.Unauthorized, "Invalid username or password ");
                }
            }

            

        }
        
    }
}
