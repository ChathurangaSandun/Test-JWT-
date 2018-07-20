using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using jwt.Infastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace jwt.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IJwtFactory jwtFactory;
        private readonly JwtIssuerOptions jwtIssuerOptions;
        

        public AuthenticationController( IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions )
        {
            this.jwtFactory = jwtFactory;
            this.jwtIssuerOptions = jwtOptions.Value;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Token()
        {
            var identity = await this.GetClaimsIdentity("sandun");
            var jwt = await TokenExtentions.GenerateJwt(identity, this.jwtFactory, "sandun", this.jwtIssuerOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity( string userName )
        {
            if ( string.IsNullOrEmpty(userName))
                return await Task.FromResult<ClaimsIdentity>(null);

            // TODO: get tokens from ENADOC and check PROTEUS database 
           
            // check the credentials
            if ( true )
            {
                return await Task.FromResult(this.jwtFactory.GenerateClaimsIdentity(userName, "5"));
            }

            //TODO when it fail
            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}