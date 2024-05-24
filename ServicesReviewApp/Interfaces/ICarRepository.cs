using ServicesReviewApp.Models;

namespace ServicesReviewApp.Interfaces
{
    public interface ICarRepository
    {
        ICollection<Car>GetCars();

        Car getCar (int id);
        Car GetCar (string Title);
        ICollection<Service> GetCarService(int id);
        bool CarExist (int id);
        bool CreateCar(Car car);
        bool UpdateCar(Car car);
        bool DeleteCar(Car car);
        bool Save();

    }
}
