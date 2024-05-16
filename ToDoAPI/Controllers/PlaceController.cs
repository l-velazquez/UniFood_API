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

        [HttpGet("{universityId}")]
        public async Task<ActionResult<List<Place>>> GetPlaces(int universityId, [FromQuery] int? placeId = null)
        {
            try
            {
                var places = await _placesService.Get(universityId, placeId);
                if (placeId.HasValue)
                {
                    var place = places.SingleOrDefault();
                    if (place == null)
                    {
                        return NotFound();
                    }
                    return Ok(place);
                }
                else
                {
                    if (places == null || places.Count == 0)
                    {
                        return NotFound();
                    }
                    return Ok(places);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
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