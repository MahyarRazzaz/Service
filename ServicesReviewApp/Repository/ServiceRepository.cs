using ServicesReviewApp.Data;
using ServicesReviewApp.Interfaces;
using ServicesReviewApp.Models;

namespace ServicesReviewApp.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DataContext context;

        public ServiceRepository(DataContext context) 
        {
            this.context = context;
        }

        public Service GetService(int id)
        {
            return context.Services.Where(S => S.ServiceId == id).FirstOrDefault();
        }

        public Service GetService(string Title)
        {
            return context.Services.Where(s => s.ServiceTitle == Title).FirstOrDefault();
        }

        public ICollection<Service> GetServices()
        {
            return context.Services.OrderBy(s => s.ServiceId).ToList();
        }

        public bool ServiceExist(int id)
        {
            return context.Services.Any(s => s.ServiceId == id);
        }
    }
}
