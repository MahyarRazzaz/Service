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
        /* [HttpGet]
         [ProducesResponseType(200, Type = typeof(IEnumerable<ServiceDto>))]
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

             var service= datamodel.Select(S=> new ServiceDto { ServiceId=S.ServiceId,ServiceTitle=S.ServiceTitle,ServiceNumber=S.ServiceNumber,ServiceDate=S.ServiceDate,Wage=S.Wage,CustomerId=S.CustomerId,CarId=S.CarId,
                 ServicesDetails = S.ServicesDetails.Select(detail => new ServiceDetailDto
                 {
                     //ServiceId = detail.ServiceId,
                     //ServicesDetailId = detail.ServicesDetailId,
                     ServiceTypeId = detail.ServiceTypeId,
                     PartId = detail.PartId,
                     Wage = detail.Wage,
                     PartPrice = detail.PartPrice
                 }).ToList()
             });


             if (!ModelState.IsValid)
                 return BadRequest(ModelState);

             return Ok(service);
         }
         */
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ServiceDto>))]
        public IActionResult Getparts()
        {
            var services = serviceRepository.GetServices()
                                             .Select(s => new ServiceDto
                                             {
                                                 ServiceId = s.ServiceId,
                                                 ServiceTitle = s.ServiceTitle,
                                                 ServiceNumber = s.ServiceNumber,
                                                 ServiceDate = s.ServiceDate,
                                                 Wage = s.Wage,
                                                 CustomerId = s.CustomerId,
                                                 CarId = s.CarId,
                                                 ServicesDetails = s.ServicesDetails.Select(detail => new ServiceDetailDto
                                                 {
                                                     ServiceTypeId = detail.ServiceTypeId,
                                                     PartId = detail.PartId,
                                                     Wage = detail.Wage,
                                                     PartPrice = detail.PartPrice
                                                 }).ToList()
                                             })
                                             .ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(services);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ServiceDto))]
        [ProducesResponseType(400)]
        public IActionResult GetService(int id)
        {
            if (!serviceRepository.ServiceExist(id))
                return NotFound();

            var service = serviceRepository.GetServices()
                                            .Where(s => s.ServiceId == id)
                                            .Select(s => new ServiceDto
                                            {
                                                ServiceId = s.ServiceId,
                                                ServiceTitle = s.ServiceTitle,
                                                ServiceNumber = s.ServiceNumber,
                                                ServiceDate = s.ServiceDate,
                                                Wage = s.Wage,
                                                CustomerId = s.CustomerId,
                                                CarId = s.CarId,
                                                ServicesDetails = s.ServicesDetails.Select(detail => new ServiceDetailDto
                                                {
                                                    ServiceTypeId = detail.ServiceTypeId,
                                                    PartId = detail.PartId,
                                                    Wage = detail.Wage,
                                                    PartPrice = detail.PartPrice
                                                }).ToList()
                                            })
                                            .FirstOrDefault();

            if (service == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(service);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Createservice([FromBody] ServiceDto serviceCreate)
        {
            if (serviceCreate == null)
                return BadRequest(ModelState);


            var service = serviceRepository.GetServices().Where(s => s.ServiceId == serviceCreate.ServiceId).FirstOrDefault();

            if (service != null)
            {
                ModelState.AddModelError("", "service already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            // تبدیل ServiceDetailDto به ServicesDetail
            var serviceDetails = new List<ServicesDetail>();
            foreach (var detailDto in serviceCreate.ServicesDetails)
            {
                var detail = new ServicesDetail
                {
                    //ServiceId = detailDto.ServiceID,
                    //ServicesDetailId= detailDto.ServicesDetailId,
                    ServiceTypeId= detailDto.ServiceTypeId,
                    PartId=detailDto.PartId,
                    Wage=detailDto.Wage,
                    PartPrice=detailDto.PartPrice,
                   
                };
                serviceDetails.Add(detail);
            }


            var servicemap = new Service
            {
                //ServiceId = serviceCreate.ServiceId,
                ServiceTitle = serviceCreate.ServiceTitle,
                Wage = serviceCreate.Wage,
                ServiceNumber = serviceCreate.ServiceNumber,
                ServiceDate = serviceCreate.ServiceDate,
                CustomerId = serviceCreate.CustomerId,
                CarId= serviceCreate.CarId,
                ServicesDetails= serviceDetails,
            };


            if (!serviceRepository.CreateService(servicemap))
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
        public IActionResult Updateservice( [FromBody] ServiceDto updateservice)
        {
            if (updateservice == null)
                return BadRequest(ModelState);

            /*if (serviceid != updateservice.ServiceId)
                return BadRequest(ModelState);*/

            if (!serviceRepository.ServiceExist(updateservice.ServiceId))
            return NotFound();
            var existservice = serviceRepository.GetService(updateservice.ServiceId);
            if (existservice == null)
                return NotFound();



            //maping
            existservice.ServiceId=updateservice.ServiceId;
            existservice.Wage=updateservice.Wage;
            existservice.ServiceDate=updateservice.ServiceDate;
            existservice.ServiceNumber=updateservice.ServiceNumber;
            existservice.ServiceTitle=updateservice.ServiceTitle;
            

            if (!serviceRepository.UpdateService(existservice))
            {
                ModelState.AddModelError("", "Something went wrong updating service");
                return StatusCode(500, ModelState);
            }
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
