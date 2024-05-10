using ServicesReviewApp.Models;

namespace ServicesReviewApp.Interfaces
{
    public interface IServiceRepository
    {
        ICollection<Service> GetServices();
        Service GetService(int id);
        Service GetService(string Title);

    }
}
