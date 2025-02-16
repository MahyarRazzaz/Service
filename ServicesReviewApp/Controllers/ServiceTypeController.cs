using Microsoft.AspNetCore.Mvc;
using ServicesReviewApp.Dto;
using ServicesReviewApp.Interfaces;
using ServicesReviewApp.Models;
using ServicesReviewApp.Repository;

namespace ServicesReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController :Controller
    {
        private readonly IServiceTypeRepository serviceTypeRepository;

        public ServiceTypeController(IServiceTypeRepository serviceTypeRepository) 
        {
            this.serviceTypeRepository = serviceTypeRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ServiceType>))]
        public IActionResult GetServiceTypes()
        {
           
            var ServiceType= serviceTypeRepository.GetServiceTypes();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ServiceType);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ServiceType))]
        [ProducesResponseType(400)]
        public IActionResult GetServiceTypes(int id)
        {

            if (!serviceTypeRepository.ServicTypeExist(id))
                return NotFound();

           
            var datamodel = serviceTypeRepository.GetServiceTypes();
            var ServiceType =datamodel.Select(s=>new ServiceTypeDto { ServiceTypeId = s.ServiceTypeId,ServiceTypeTitle=s.ServiceTypeTitle });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ServiceType);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateServiceType([FromBody] ServiceTypeDto servicecreate)
        {
            if (servicecreate == null)
                return BadRequest(ModelState);

            
            var servicetype = serviceTypeRepository.GetServiceTypes().Where(s=>s.ServiceTypeId==servicecreate.ServiceTypeId).FirstOrDefault();

            if (servicetype != null)
            {
                ModelState.AddModelError("", "ServiceType already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var servicetypemap = new ServiceType
            {
               // ServiceTypeId = servicecreate.ServiceTypeId,
               ServiceTypeTitle = servicecreate.ServiceTypeTitle,
               
            };

            if (!serviceTypeRepository.CreateServiceType(servicetypemap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }


            return Ok("Successfully created");
        }
        [HttpPut("{servicetypeid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateServiceType(int servicetypeid, [FromBody] ServiceTypeDto updateservicetype)
        {
            if (updateservicetype == null)
                return BadRequest(ModelState);

            if (servicetypeid != updateservicetype.ServiceTypeId)
                return BadRequest(ModelState);

            if (!serviceTypeRepository.ServicTypeExist(servicetypeid))
            return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();
            var existingservicetype=serviceTypeRepository.GetServiceType(servicetypeid);
            if (existingservicetype == null) return NotFound();

            //manually map
            existingservicetype.ServiceTypeId = updateservicetype.ServiceTypeId;
            existingservicetype.ServiceTypeTitle=updateservicetype.ServiceTypeTitle;
           

            if (!serviceTypeRepository.UpdateServiceType(existingservicetype))
            {
                ModelState.AddModelError("", "Something went wrong updating part");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{servicetypeid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteServiceType(int servicetypeid)
        {
            if (!serviceTypeRepository.ServicTypeExist(servicetypeid))
            {
                return NotFound();
            }

            var servicetypeToDelete = serviceTypeRepository.GetServiceType(servicetypeid);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (serviceTypeRepository.DeleteServiceType(servicetypeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting service");
            }

            return NoContent();
        }
    }
}
