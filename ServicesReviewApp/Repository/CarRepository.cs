

using Microsoft.EntityFrameworkCore;
using ServicesReviewApp.Data;
using ServicesReviewApp.Interfaces;
using ServicesReviewApp.Models;

namespace ServicesReviewApp.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext context;

        public CarRepository(DataContext context)
        {
           this.context = context;
        }
        public Car getCar(int id)
        {
            return context.Cars.Where(c => c.CarId == id).FirstOrDefault();
        }

        public Car GetCar(string Title)
        {
            return context.Cars.Where(c => c.CarTitle == Title).FirstOrDefault();
        }
        //get service question
        public ICollection<Service> GetCarService(int id)
        {
            return context.Services.Where(s => s.CarId == id).ToList();
        }
        
        public ICollection<Car>GetCars()

        { 
            return context.Cars.OrderBy(c=>c.CarId).ToList();
        }
        public bool CarExist(int id)
        {
            return context.Cars.Any(c => c.CarId == id);
        }

        public bool CreateCar(Car car)
        {
            context.Add(car);
            return Save();
        }

        public bool UpdateCar(Car car)
        {
           context.Update(car);
            return Save();
        }

        public bool DeleteCar(Car car)
        {
            context.Remove(car);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false; 
        }
    }
}
