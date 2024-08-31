using Microsoft.AspNetCore.Mvc;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public String Get()
        {
            return "Teste Stefanini - João Victor Cordeiro - 30/08/2024";
        }
    }
}
