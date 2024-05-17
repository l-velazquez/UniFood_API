using Microsoft.AspNetCore.Mvc;
using UniFood.Attributes;
using UniFood.Models;
using UniFood.Services;
using Microsoft.AspNetCore.Authorization;

namespace UniFood.Controllers
{
    public class FavoritesController : BaseController
    {
        private readonly ILogger<FavoritesController> _logger;
        private readonly FavoritesService _favoritesService;

        public FavoritesController(ILogger<FavoritesController> logger, FavoritesService favoritesService)
        {
            _logger = logger;
            _favoritesService = favoritesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Favorite>>> GetAll()
        {
            try
            {
                return Ok(await _favoritesService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Favorite>>> Get(int id)
        {
            try
            {
                return Ok(await _favoritesService.Get(id));
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<Favorite>> Post(Favorite favorite)
        {
            try
            {
                return Ok(await _favoritesService.Post(favorite));
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Favorite>> Delete(int id)
        {
            try
            {
                return Ok(await _favoritesService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
            }
        }
    }
}