using Microsoft.EntityFrameworkCore;
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

        public bool CreateService(Service service)
        {
           context.Add(service);
            return Save();
        }

        public bool DeleteService(Service service)
        {
            context.Remove(service);
            return Save();
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
            return context.Services.Include(s=>s.ServicesDetails).OrderBy(s => s.ServiceId).ToList(); 
        }


        /*public ICollection<Service> GetServices()
        {
            return context.Services
                           .Include(s => s.ServicesDetails) // Load related data
                           .ToList(); // Convert to a concrete collection (ICollection)
        }*/

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ServiceExist(int id)
        {
            return context.Services.Any(s => s.ServiceId == id);
        }

        public bool UpdateService(Service service)
        {
            context.Update(service);
            return Save();
        }
    }
}
