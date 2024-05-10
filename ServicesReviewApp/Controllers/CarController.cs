using Microsoft.AspNetCore.Mvc;
using ServicesReviewApp.Dto;
using ServicesReviewApp.Interfaces;
using ServicesReviewApp.Models;

namespace ServicesReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarRepository carRepository;

        public CarController(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCars()
        {
            var cars = carRepository.GetCars();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cars);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Car))]
        [ProducesResponseType(400)]
        public IActionResult GetCar(int id)
        {
            if (!carRepository.CarExist(id))
                return NotFound();
            var datamodel = carRepository.GetCars();

            var Car = datamodel.Select(c => new CarDto { CarId = c.CarId, CarTitle = c.CarTitle, ChassisNumber = c.ChassisNumber, PlatsNumber = c.PlatsNumber });
                
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Car);
        }

    }
}
