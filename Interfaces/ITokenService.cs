using Jobs2.Modules;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Jobs2.interfaces
{
    public interface ITokenService
    {

        SecurityToken GetToken(List<Claim> claims);

        //TokenValidationParameters GetTokenValidationParameters();

        string WriteToken(SecurityToken token);
    }
}