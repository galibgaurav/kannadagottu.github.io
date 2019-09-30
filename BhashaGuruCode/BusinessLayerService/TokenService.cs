using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayerService
{
    public class TokenService : ITokenService
    {
        public string GetToken(/*string role*/) //TODO-  Read from db, add column in the azure db
        {
            //Security key
            string securityKey = "2019_bhashaguru_Break_Language_Bar$#@connectedSkills.in"; //TODO put in config

            //SymmetricSecurityKey
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //signing credential
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //create token
            IList<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>();
            claims.Add(new System.Security.Claims.Claim(ClaimTypes.Role,"appuser"));//TODO-  Read from db, Use from 
            //claims.Add(new System.Security.Claims.Claim(ClaimTypes.Email, "UserEmail"));
            //claims.Add(new System.Security.Claims.Claim(ClaimTypes.MobilePhone, "UserMobileNumber"));
            var token = new JwtSecurityToken(
                issuer: "connectedSkills.in",
                audience: "AdminAndAppUser",
                expires: DateTime.Now.AddMonths(3),
                signingCredentials: signingCredentials,
                claims: claims
                );

            var securityToken = new JwtSecurityTokenHandler().WriteToken(token);

            return securityToken;
        }
    }
}
