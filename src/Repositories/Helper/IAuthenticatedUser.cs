using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Helper
{
    public interface IAuthenticatedUser
    {
        int UserId { get; }
        string UserName { get; }
        string Token { get; }
        List<string> Roles { get; }
    }
}
