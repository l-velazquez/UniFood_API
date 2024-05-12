using Microsoft.AspNetCore.Mvc;
using UniFood.Attributes;

namespace UniFood.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiKey]
    [ApiController]
    public class BaseController : ControllerBase
    {

    }
}
