using ExampleAuthJwt.Model;
using ExampleAuthJwt.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAuthJwt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public WeatherForecastController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public string Token1()
        {
            
            var user = new User(
                Id: 1, 
                Email: "exemplo@dominio.com", 
                Password: "senhaSegura123", 
                Roles: new[] { "User", "Admin" } 
            );

            // Gere o token usando o serviço
            var token = _tokenService.Generate(user);
            return token;
        }
    }
}
