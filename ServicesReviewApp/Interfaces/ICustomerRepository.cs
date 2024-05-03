using ServicesReviewApp.Models;

namespace ServicesReviewApp.Interfaces
{
    public interface ICustomerRepository
    {
        ICollection<Customer> GetCustomers();
    }
}
