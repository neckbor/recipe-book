using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Core_3._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetRecipeController : ControllerBase
    {
        [HttpPost]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult NextStep()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PreviousStep()
        {
            return Ok();
        }
    }
}