using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace jwt.Infastructure
{
    public class TokenExtentions 
    {
        public static async Task<string> GenerateJwt( ClaimsIdentity identity, IJwtFactory jwtFactory, string userName,
           JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings )
        {
            var response = new
            {
                id = "5",               
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
