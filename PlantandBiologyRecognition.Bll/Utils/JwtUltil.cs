using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlantandBiologyRecognition.DAL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Utils
{
    public class JwtUtil
    {
        private JwtUtil()
        {
        }

        //public static string GenerateJwtToken(Account account, Tuple<string, Guid> guidClaim)
        //{
        //    //IConfiguration configuration = new ConfigurationBuilder()
        //    //    .AddEnvironmentVariables().Build();
        //    JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
        //    SymmetricSecurityKey secrectKey =
        //        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secrect"));
        //    var credentials = new SigningCredentials(secrectKey, SecurityAlgorithms.HmacSha256Signature);
        //    string issuer = "issue";
        //    List<Claim> claims = new List<Claim>()
        //{
        //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //    new Claim(JwtRegisteredClaimNames.Sub, account.Phone.ToString()),
        //    new Claim(ClaimTypes.Role, account.Roleid),
        //};
        //    if (guidClaim != null) claims.Add(new Claim(guidClaim.Item1, guidClaim.Item2.ToString()));
        //    var expires = DateTime.Now.AddHours(6);

        //    var token = new JwtSecurityToken(issuer, null, claims, notBefore: DateTime.Now, expires, credentials);
        //    return jwtHandler.WriteToken(token);
        //}
    }
}
