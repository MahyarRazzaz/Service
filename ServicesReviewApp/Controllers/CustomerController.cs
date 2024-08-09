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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCustomer([FromBody] CustomerDto customerCreate)
        {
            if (customerCreate == null)
                return BadRequest(ModelState);

            var customer = customerRepository.GetCustomers()
                .Where(c=>c.CustomerId == customerCreate.CustomerId)
                .FirstOrDefault();

            if (customer != null)
            {
                ModelState.AddModelError("", "Customer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var customermap = new Customer
            {
                //CustomerId = customerCreate.CustomerId,
                FirstName = customerCreate.FirstName,
                LastName = customerCreate.LastName,
            };
            if (!customerRepository.CreateCustomer(customermap))
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
        public IActionResult UpdateCaustomer( [FromBody] CustomerDto updatecustomer)
        {
            if (updatecustomer == null)
                return BadRequest(ModelState);

           /* if (customerid != updatecustomer.CustomerId)
                return BadRequest(ModelState);*/

            if (!customerRepository.customerExist(updatecustomer.CustomerId));
                return NotFound();

            /*if (!ModelState.IsValid)
                return BadRequest();*/

            var existingcustomer = customerRepository.GetCustomerById(updatecustomer.CustomerId);
            if (existingcustomer == null) return NotFound();

            //manually map
            existingcustomer.CustomerId = updatecustomer.CustomerId;
            existingcustomer.FirstName = updatecustomer.FirstName;
            existingcustomer.LastName = updatecustomer.LastName;
           
            

            if (!customerRepository.UpdateCustomer(existingcustomer))
            {
                ModelState.AddModelError("", "Something went wrong updating customer");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{customerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCustomer(int customerid)
        {
            if (!customerRepository.customerExist(customerid))
            {
                return NotFound();
            }

            var customerToDelete = customerRepository.GetCustomerById(customerid);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!customerRepository.DeleteCustomer(customerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting customer");
            }

            return NoContent();
        }

    }
}
