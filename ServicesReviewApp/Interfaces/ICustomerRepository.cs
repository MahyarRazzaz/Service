using ServicesReviewApp.Models;

namespace ServicesReviewApp.Interfaces
{
    public interface ICustomerRepository
    {
        ICollection<Customer> GetCustomers();

        Customer GetCustomerById(int id);
        Customer GetCustomerByName(string name);
        ICollection<Service> GetCarService(int id);
        bool customerExist(int id);
    }
}
