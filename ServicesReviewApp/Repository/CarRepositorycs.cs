

using Microsoft.EntityFrameworkCore;
using ServicesReviewApp.Data;
using ServicesReviewApp.Interfaces;
using ServicesReviewApp.Models;

namespace ServicesReviewApp.Repository
{
    public class CarRepositorycs : ICarRepository
    {
        private readonly DataContext context;

        public CarRepositorycs(DataContext context)
        {
            context = context;
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
        public Car GetCarService(int id)
        {
            var service = context.Services.Where(S => S.CarId == id).FirstOrDefault();
            if (service != null) {}
            return null;
         
        }
        
        public ICollection<Car>GetCars()

        { 
            return context.Cars.OrderBy(c=>c.CarId).ToList();
        }
        public bool CarExist(int id)
        {
            return context.Cars.Any(c => c.CarId == id);
        }



    }
}
