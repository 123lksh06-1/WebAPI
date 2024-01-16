using Microsoft.AspNetCore.Mvc;
using WebAPI.Filter.ActionFilters;
using WebAPI.Filter.ExceptionFilters;
using WebAPI.Model;
using WebAPI.Model.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase //to make this as an web api controller class we need to include two things:ControllerBase and[ApiController]
    {
        [HttpGet]
        [Route("/shirts")]
        public string GetShirts()
        {
            return "Reading all the shirts";
        }
        [HttpGet("{id}/{color}")]
        [Route("/shirts/id")]

        public IActionResult GetShirtById(int id)
        {
            return Ok(HttpContext.Items["shirt"]);
        }
        [HttpPost]
        [Shirt_ValidateShirtIdFilter]

        public IActionResult CreateShirt([FromBody] Shirt shirt)
        { if (shirt == null) return BadRequest();
            var existingShirt = ShirtRepository.GetShirtsByProperties(shirt.Brand, shirt.Gender, shirt.Color, shirt.Size);
            if (existingShirt != null) return BadRequest();//not null means we already have matching shirt and will not create a duplicate
            ShirtRepository.AddShirt(shirt);
            return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ShirtId }, shirt);
        }
        [HttpPut("{id}")]
        [Shirt_ValidateShirtIdFilter]
        [Shirt_ValidateUpdateShirtFilter]
        [Shirt_HandleUpdateExceptionsFilter]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            ShirtRepository.UpdateShirt(shirt);
            // try (removing try and catch statements as included exception filter)
            // } catch{if(!ShirtRepository.ShirtExists(id))return NotFound();throw;}
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteShirt(int id) {
            var shirt = ShirtRepository.GetShirtById(id);
            ShirtRepository.DeleteShirt(id);
            return Ok(shirt);
        }
    }
}
