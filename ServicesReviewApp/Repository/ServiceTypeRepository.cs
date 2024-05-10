using ServicesReviewApp.Data;
using ServicesReviewApp.Interfaces;
using ServicesReviewApp.Models;

namespace ServicesReviewApp.Repository
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly DataContext context;

        public ServiceTypeRepository(DataContext context) 
        {
            this.context = context;
        }

        public ICollection<ServicesDetail> GetServicesDetailType(int id)
        {
            return context.ServicesDetails.Where(S=>S.ServiceTypeId==id).ToList();
        }

        public ServiceType GetServiceType(int id)
        {
            return context.ServiceTypes.Where(s => s.ServiceTypeId == id).FirstOrDefault();
        }

        public ServiceType GetServiceType(string Title)
        {
            return context.ServiceTypes.Where(s => s.ServiceTypeTitle == Title).FirstOrDefault();
        }

        public ICollection<ServiceType> GetServiceTypes()
        {
            return context.ServiceTypes.OrderBy(s=>s.ServiceTypeId).ToList();
        }

        public bool servicTypeExist(int id)
        {
            return context.ServiceTypes.Any(s => s.ServiceTypeId==id);
        }
    }
}
