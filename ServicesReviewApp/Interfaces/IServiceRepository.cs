using ServicesReviewApp.Models;

namespace ServicesReviewApp.Interfaces
{
    public interface IServiceRepository
    {
        ICollection<Service> GetServices();
        Service GetService(int id);
        Service GetService(string Title);
        bool ServiceExist (int id);
        bool CreateService(Service service);
        bool UpdateService (Service service);
        bool DeleteService (Service service);
        bool Save();
    }
}
