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

            if (!serviceTypeRepository.servicTypeExist(id))
                return NotFound();

           
            var datamodel = serviceTypeRepository.GetServiceTypes();
            var ServiceType =datamodel.Select(s=>new ServiceTypeDto { ServiceTypeId = s.ServiceTypeId,ServiceTypeTitle=s.ServiceTypeTitle });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ServiceType);
        }
    }
}
