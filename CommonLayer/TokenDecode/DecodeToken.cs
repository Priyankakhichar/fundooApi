using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace CommonLayer.TokenDecode
{
   public class DecodeToken
    {
        public string GetEmail(string token)
        {
            var tokenString = new JwtSecurityToken(jwtEncodedString: token);
            var email = tokenString.Claims.First(c => c.Type == "Email").Value;
            return email;
        }
    }
}
