using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Config
{
    public interface IJwtAuthManager
    {
        public string GenerateToken(string username, Claim[] claims, DateTime now);
    }
}
