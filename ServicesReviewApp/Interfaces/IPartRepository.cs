using ServicesReviewApp.Models;

namespace ServicesReviewApp.Interfaces
{
    public interface IPartRepository
    {
        ICollection<Part> GetParts();
        Part GetPart(int id);
        Part GetPart(string Title);
        ICollection<Service> GetServiceByPart (int partId);
        ICollection<Part> GetPartOfService(int serviceId);
        bool PartExist(int id);
        bool CreatePart(Part part);
        bool UpdatePart(Part part);
        bool DeletePart(Part part);
        bool Save();
    }
}
