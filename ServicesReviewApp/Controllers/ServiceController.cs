﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Createservice([FromBody] ServiceDto serviceCreate)
        {
            if (serviceCreate== null)
                return BadRequest(ModelState);

            
            var service = serviceRepository.GetServices().Where(s=>s.ServiceId==serviceCreate.ServiceId).FirstOrDefault();

            if (service != null)
            {
                ModelState.AddModelError("", "service already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok("Successfully created");
        }
        [HttpPut("{serviceid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Updateservice(int serviceid, [FromBody] ServiceDto updateservice)
        {
            if (updateservice == null)
                return BadRequest(ModelState);

            if (serviceid != updateservice.ServiceId)
                return BadRequest(ModelState);

            if (!serviceRepository.ServiceExist(serviceid));
            return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            return NoContent();
        }
        [HttpDelete("{serviceid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteService(int serviceid)
        {
            if (!serviceRepository.ServiceExist(serviceid))
            {
                return NotFound();
            }

            var serviceToDelete = serviceRepository.GetService(serviceid);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (serviceRepository.DeleteService(serviceToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting service");
            }

            return NoContent();
        }
    }
}
