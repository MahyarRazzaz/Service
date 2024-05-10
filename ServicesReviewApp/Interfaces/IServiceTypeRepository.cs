using ServicesReviewApp.Models;

namespace ServicesReviewApp.Interfaces
{
    public interface IServiceTypeRepository
    {
        ICollection<ServiceType> GetServiceTypes();
        ServiceType GetServiceType(int id);
        ServiceType GetServiceType(string Title);
        ICollection<ServicesDetail> GetServicesDetailType(int id);
        bool servicTypeExist (int id);
    }
}
