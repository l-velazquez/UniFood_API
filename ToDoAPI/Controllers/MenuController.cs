using Microsoft.AspNetCore.Mvc;
using UniFood.Attributes;
using UniFood.Models;
using UniFood.Services;
using Microsoft.AspNetCore.Authorization;


namespace UniFood.Controllers
{

    public class MenusController : BaseController
    {
        private readonly ILogger<MenusController> _logger;
        private readonly MenusService _menusService;

        public MenusController(ILogger<MenusController> logger, MenusService menusService)
        {
            _logger = logger;
            _menusService = menusService;
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<List<Menu>>> Get(int id)
        {
            try
            {
                return Ok(await _menusService.Get(id));
                
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
                
            }
        }

        [HttpPost]
        public async Task<ActionResult<Menu>> Post(Menu menu)
        {
            try
            {
                return Ok(await _menusService.Post(menu));
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));

            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put(Menu menu)
        {
            try
            {
                return Ok(await _menusService.Put(menu));
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Menu>> Delete(int id)
        {
            try
            {
                return Ok(await _menusService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));

            }
        }
    }
}