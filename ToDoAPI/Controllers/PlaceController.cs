using Microsoft.AspNetCore.Mvc;
using UniFood.Attributes;
using UniFood.Models;
using UniFood.Services;
using Microsoft.AspNetCore.Authorization;


namespace UniFood.Controllers
{

    public class PlacesController : BaseController
    {
        private readonly ILogger<PlacesController> _logger;
        private readonly PlacesService _placesService;

        public PlacesController(ILogger<PlacesController> logger, PlacesService placesService)
        {
            _logger = logger;
            _placesService = placesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Place>>> GetAll()
        {
            try
            {
                return Ok(await _placesService.GetAll());
                
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
                
            }
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<List<Place>>> Get(int id)
        {
            try
            {
                return Ok(await _placesService.Get(id));
                
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
                
            }
        }

        [HttpPost]
        public async Task<ActionResult<Place>> Post(Place place)
        {
            try
            {
                return Ok(await _placesService.Post(place));
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));

            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put(Place place)
        {
            try
            {
                return Ok(await _placesService.Put(place));
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Place>> Delete(int id)
        {
            try
            {
                return Ok(await _placesService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));

            }
        }
    }
}