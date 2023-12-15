using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.IntegrationTests.Util
{
    public class AuthHelper
    {
        public static Dictionary<string, object> GetBearerForUser(string username)
        {
            return new Dictionary<string, object> { { ClaimTypes.Name, username } };
        }
    }
}
