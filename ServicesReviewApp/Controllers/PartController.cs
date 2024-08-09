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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePart(  [FromBody] PartDto partCreate)
        {
            if (partCreate == null)
                return BadRequest(ModelState);

            var part = partRepository.GetParts().Where(p => p.PartId == partCreate.PartId).FirstOrDefault();

            if (part != null)
            {
                ModelState.AddModelError("", "part already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var partmap = new Part
            {
               // PartId = partCreate.PartId,
                PartTitle = partCreate.PartTitle,
            };

            if (!partRepository.CreatePart(partmap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePart( [FromBody] PartDto updatepart)
        {
            if (updatepart == null)
                return BadRequest(ModelState);

            /*if (partid != updatepart.PartId)
                return BadRequest(ModelState);*/

            if (!partRepository.PartExist(updatepart.PartId)) ;
            return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();


            var existingpart = partRepository.GetPart(updatepart.PartId);
            if (existingpart == null) return NotFound();

            //manually map
            existingpart.PartId=updatepart.PartId;
            existingpart.PartTitle=updatepart.PartTitle;
            


            if (!partRepository.UpdatePart(existingpart))
            {
                ModelState.AddModelError("", "Something went wrong updating part");
                return StatusCode(500, ModelState);
            }
                return NoContent();
        }
        [HttpDelete("{partid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePart(int partid)
        {
            if (!partRepository.PartExist(partid))
            {
                return NotFound();
            }

            var partToDelete = partRepository.GetPart(partid);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (partRepository.DeletePart(partToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting part");
            }

            return NoContent();
        }
      }
    }
