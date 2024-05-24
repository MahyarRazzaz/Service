using Microsoft.EntityFrameworkCore;
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
            this.context = context;
        }

        public bool CreatePart(Part part)
        {
            context.Add(part);
            return Save();
        }

        public bool DeletePart(Part part)
        {
            context.Remove(part);
            return Save();
        }

        public Part GetPart(int id)
        {
            return context.parts.Where(p => p.PartId == id).FirstOrDefault();
        }

        public Part GetPart(string Title)
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

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePart(Part part)
        {
            context.Update(part);
            return Save();
        }
    }
}
