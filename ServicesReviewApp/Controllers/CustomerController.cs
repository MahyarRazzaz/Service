using Microsoft.AspNetCore.Mvc;
using ServicesReviewApp.Dto;
using ServicesReviewApp.Interfaces;
using ServicesReviewApp.Models;
using ServicesReviewApp.Repository;

namespace ServicesReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
        public IActionResult GetCustomer()
        {
            var Customers = customerRepository.GetCustomers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Customers);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        public IActionResult GetCustomer(int id)
        {
            if (!customerRepository.customerExist(id))
                return NotFound();
            var datamodel = customerRepository.GetCustomers();

            var customer = datamodel.Select(c=>new CustomerDto {CustomerId=c.CustomerId,FirstName=c.FirstName,LastName=c.LastName});
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(customer);
        }
    }
}
