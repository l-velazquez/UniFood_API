using Microsoft.AspNetCore.Mvc;
using UniFood.Attributes;
using UniFood.Models;
using UniFood.Services;
using Microsoft.AspNetCore.Authorization;
using UniFood.Models;


namespace UniFood.Controllers
{
    [ApiController]
    public class UniversitiesController : BaseController
    {
        private readonly ILogger<UniversitiesController> _logger;
        private readonly UniversitiesService _universitiesService;

        public UniversitiesController(ILogger<UniversitiesController> logger, UniversitiesService universitiesService)
        {
            _logger = logger;
            _universitiesService = universitiesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<University>>> GetAll()
        {
            try
            {
                return Ok(await _universitiesService.GetAll());
                
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
                
            }
        }



        [HttpGet("{id}")] 
        public async Task<ActionResult<List<University>>> Get(int id)
        {
            try
            {
                return Ok(await _universitiesService.Get(id));
                
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));
                
            }
        }

        [HttpPost]
        public async Task<ActionResult<University>> Post(University university)
        {
            try
            {
                return Ok(await _universitiesService.Post(university));
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));

            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put(University university)
        {
            try
            {
                return Ok(await _universitiesService.Put(university));
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<University>> Delete(int id)
        {
            try
            {
                return Ok(await _universitiesService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(Content(e.Message, "application/json"));

            }
        }
    }
}