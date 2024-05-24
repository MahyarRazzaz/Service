using ServicesReviewApp.Models;

namespace ServicesReviewApp.Interfaces
{
    public interface IServiceTypeRepository
    {
        ICollection<ServiceType> GetServiceTypes();
        ServiceType GetServiceType(int id);
        ServiceType GetServiceType(string Title);
        ICollection<ServicesDetail> GetServicesDetailType(int id);
        bool ServicTypeExist (int id);
        bool CreateServiceType(ServiceType serviceType);
        bool UpdateServiceType(ServiceType serviceType);
        bool DeleteServiceType(ServiceType serviceType);
        bool Save();
    }
}
