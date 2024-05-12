using Microsoft.AspNetCore.Mvc;
using UniFood.Models;
using UniFood.Services;

namespace UniFood.Controllers
{
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly LoginService _loginService;
        public LoginController(
            LoginService loginService
        )
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> LoginAsync([FromBody] Login login)
        {
            try
            {
                return Ok(await _loginService.LoginAsync(login));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
