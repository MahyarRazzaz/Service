using Microsoft.AspNetCore.Mvc;
using ServicesReviewApp.Dto;
using ServicesReviewApp.Interfaces;
using ServicesReviewApp.Models;
using ServicesReviewApp.Repository;

namespace ServicesReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController :Controller
    {
        private readonly IServiceRepository serviceRepository;

        public ServiceController(IServiceRepository serviceRepository) 
        {
            this.serviceRepository = serviceRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Service>))]
        public IActionResult Getparts()
        {
            
            var service = serviceRepository.GetServices();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(service);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Service))]
        [ProducesResponseType(400)]
        public IActionResult GetService(int id)
        {

           
            if (!serviceRepository.ServiceExist(id))
                return NotFound();

            var datamodel = serviceRepository.GetServices();

            var service= datamodel.Select(S=> new ServiceDto { ServiceId=S.ServiceId,ServiceTitle=S.ServiceTitle,ServiceNumber=S.ServiceNumber,ServiceDate=S.ServiceDate,Wage=S.Wage,CustomerId=S.CustomerId,CarId=S.CarId});


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(service);
        }
    }
}
