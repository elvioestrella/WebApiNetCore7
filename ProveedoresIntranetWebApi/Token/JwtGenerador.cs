using Microsoft.IdentityModel.Tokens;
using ProveedoresIntranetWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProveedoresIntranetWebApi.Token
{
    public class JwtGenerador : IJwtGenerador
    {
        public string CrearToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName!),
                new Claim("userId", usuario.Id),
                new Claim("email", usuario.Email!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Extranet para Proveedores de Hipermercados Ole. @HiperM3rcad02 01e ##3554"));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credenciales,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescripcion);

            return tokenHandler.WriteToken(token);
        }
    }
}
