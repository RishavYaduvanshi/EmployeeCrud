using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using EmployeeDetails.DB;
using System.Collections;

namespace EmployeeDetails.BasicAuth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly EmployeeDbContext _dbContext;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            EmployeeDbContext dbContext)
            : base(options, logger, encoder, clock)
        {
            _dbContext = dbContext;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var email = credentials[0];
                var password = credentials[1];

                var user = _dbContext.SignUps.FirstOrDefault(u => u.Email == email && u.Password == password);

                if (user == null)
                {
                    return AuthenticateResult.Fail("Invalid Username or Password");
                }
                else
                {
                    var rolelist = _dbContext.SignUpsRoles
                                   .Where(s => s.SignUpId == user.UserId)
                                   .Select(r => r.RoleId)
                                   .ToList();


                    var roles = new List<string>();
                    foreach (var role in rolelist)
                    {
                        var roleName = _dbContext.Roles.FirstOrDefault(r => r.RoleId == role)?.RoleName;
                        if (!string.IsNullOrEmpty(roleName))
                        {
                            Console.Out.WriteLine(roleName);
                            roles.Add(roleName);
                        }
                    }

                    string oneRollName = null;
                    if (rolelist.Count > 0)
                    {
                        oneRollName = roles[0];
                    }

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    //claims.Add(new Claim(ClaimTypes.Role, oneRollName));

                    foreach (var role in roles)
                    {
                        Console.Out.WriteLine("the " + role);
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    Console.Out.WriteLine(claims.ToString());
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }
    }
}
