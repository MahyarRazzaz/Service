using Microsoft.AspNetCore.Mvc;
using ServicesReviewApp.Dto;
using ServicesReviewApp.Interfaces;
using ServicesReviewApp.Models;
using ServicesReviewApp.Repository;

namespace ServicesReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartController : Controller
    {
        private readonly IPartRepository partRepository;

        public PartController(IPartRepository partRepository)
        {
            this.partRepository = partRepository;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Part>))]
        public IActionResult Getparts()
        {
            var part = partRepository.GetParts();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(part);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Part))]
        [ProducesResponseType(400)]
        public IActionResult Getpart(int id)
        {

            if (!partRepository.PartExist(id))
                return NotFound();

            var datamodel = partRepository.GetParts();

            var part = datamodel.Select(p => new PartDto { PartId = p.PartId, PartTitle = p.PartTitle });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(part);
        }
    }
}
