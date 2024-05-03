using ServicesReviewApp.Models;

namespace ServicesReviewApp.Interfaces
{
    public interface ICarRepository
    {
        ICollection<Car>GetCars();

        Car getCar (int id);
        Car GetCar (string Title);
        Car GetCarService(int id);
        bool CarExist (int id);
    }
}
