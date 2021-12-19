using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Helper
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        public int UserId { get; }
        public string UserName { get; }
        public string Token { get; }
        public List<string> Roles { get; }
        public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {            
            UserId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            UserName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            Roles = httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(x => x.Value.ToString()).ToList();
            Token = httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
        }
    }
}
