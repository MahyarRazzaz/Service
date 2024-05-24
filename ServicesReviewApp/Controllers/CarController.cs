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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCar([FromBody] CarDto carCreate)
        {
            if (carCreate == null)
                return BadRequest(ModelState);

            var car = carRepository.GetCars()
                .Where(c=>c.CarTitle.Trim().ToUpper() == carCreate.CarTitle.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (car!= null)
            {
                ModelState.AddModelError("", "Car already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /*var carmap = _mapper.Map<Car>(carCreate);

                if (!carRepository.CreateCar(carmap))
                {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }*/

            return Ok("Successfully created");
        }


        [HttpPut("{carid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCar(int carId, [FromBody] CarDto updatecar)
        {
            if (updatecar == null)
                return BadRequest(ModelState);

            if (carId != updatecar.CarId)
                return BadRequest(ModelState);

            if(!carRepository.CarExist(carId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

           /* var carMap = _mapper.Map<Car>(updatedcar);

            if (!carRepository.UpdateCar(carMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
           */
            return NoContent();
        }
        [HttpDelete("{carId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCar(int carid)
        {
            if (!carRepository.CarExist(carid))
            {
                return NotFound();
            }

            var carToDelete = carRepository.getCar(carid);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!carRepository.DeleteCar(carToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting car");
            }

            return NoContent();
        }

    }
}
