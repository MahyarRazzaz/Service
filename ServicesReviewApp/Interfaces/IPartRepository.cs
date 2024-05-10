using ServicesReviewApp.Models;

namespace ServicesReviewApp.Interfaces
{
    public interface IPartRepository
    {
        ICollection<Part> GetParts();
        Part GetPart(int id);
        Part getPart(string Title);
        ICollection<Service> GetServiceByPart (int partId);
        ICollection<Part> GetPartOfService(int serviceId);
        bool PartExist(int id);
    }
}
