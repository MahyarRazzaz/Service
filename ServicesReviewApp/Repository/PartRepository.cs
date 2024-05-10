using ServicesReviewApp.Data;
using ServicesReviewApp.Interfaces;
using ServicesReviewApp.Models;

namespace ServicesReviewApp.Repository
{
    public class PartRepository : IPartRepository
    {
        private readonly DataContext context;
        public PartRepository(DataContext context)
        {
            context = context;
        }

        public Part GetPart(int id)
        {
            return context.parts.Where(p => p.PartId == id).FirstOrDefault();
        }

        public Part getPart(string Title)
        {
            return context.parts.Where(p => p.PartTitle == Title).FirstOrDefault();
        }

        public ICollection<Part> GetPartOfService(int serviceId)
        {
            return context.ServicesDetails.Where(s=>s.ServiceId== serviceId).Select(p=>p.Part).ToList();
        }

        public ICollection<Part> GetParts()
        {
            return context.parts.OrderBy(p=>p.PartId).ToList();
        }

        public ICollection<Service> GetServiceByPart(int partId)
        {
            return context.ServicesDetails.Where(p=>p.PartId==partId).Select(s=>s.Service).ToList();
        }

        public bool PartExist(int id)
        {
            return context.parts.Any(p=>p.PartId==id);
        }
    }
}
