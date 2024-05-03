using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetCars()
        {
            var Customers = customerRepository.GetCustomers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Customers);
        }
    }
}
