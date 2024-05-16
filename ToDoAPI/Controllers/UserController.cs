using Microsoft.AspNetCore.Mvc;
using UniFood.Attributes;
using UniFood.Models;
using UniFood.Services;
using Microsoft.AspNetCore.Authorization;

namespace UniFood.Controllers
{

    public class UsersController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserService _userService;

        public UsersController(ILogger<UsersController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetAll()
        {
            try
            {
                return Ok(await _userService.GetAll());
                
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
                
            }
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<Users>> GetByEmail(string email)
        {
            try
            {
                return Ok(await _userService.GetByEmail(email));
                
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
                
            }
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<List<Users>>> Get(int id)
        {
            try
            {
                return Ok(await _userService.Get(id));
                
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
                
            }
        }

        [HttpPost]
        public async Task<ActionResult<Users>> Post(Users user)
        {
            try
            {
                return Ok(await _userService.Post(user));
                
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
                
            }
        }

        [HttpPut]
        public async Task<ActionResult<Users>> Put(Users user)
        {
            try
            {
                return Ok(await _userService.Put(user));
                
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
                
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> Delete(int id)
        {
            try
            {
                return Ok(await _userService.Delete(id));
                
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
                
            }
        }
    }
}