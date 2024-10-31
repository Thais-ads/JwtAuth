using ExampleAuthJwt.Model;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExampleAuthJwt.Repositories
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Generate(User user)
        {
            // Cria uma instância do JwtSecurityTokenHandler
            var handler = new JwtSecurityTokenHandler();

            // Configuração do JWT usando a chave secreta da configuração
            var key = Encoding.UTF8.GetBytes(Configuration.PrivateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            );

            // Configurações do token, incluindo data de expiração
            var tokenDescriptor = new SecurityTokenDescriptor
            {


                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddDays(2)
            };

            // Gera o token
            var token = handler.CreateToken(tokenDescriptor);

            // Retorna a string do token
            return handler.WriteToken(token);
        }



        public static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim
                (new Claim(ClaimTypes.Name, user.Email, ClaimValueTypes.String));

            foreach (var role in user.Roles)
            {
                ci.AddClaim(new Claim (ClaimTypes.Role, role));
            }
            ci.AddClaim(new Claim("Fruta", "a"));

            return ci;
        }
    }
}
