using ServicesReviewApp.Data;
using ServicesReviewApp.Interfaces;
using ServicesReviewApp.Models;

namespace ServicesReviewApp.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext context;
        public CustomerRepository(DataContext context)
        {
            context = context;
        } 
        public bool customerExist(int id)
        {
            return context.Customers.Any(c=>c.CustomerId == id);
        }

        public ICollection<Service> GetCarService(int id)
        {
            return context.Services.Where(c=>c.CustomerId==id).ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
        }

        public Customer GetCustomerByName(string name)
        {
            return context.Customers.Where(c=>c.FirstName==name).FirstOrDefault();
        }

        public ICollection<Customer> GetCustomers()
        {
            return context.Customers.OrderBy(c=>c.CustomerId).ToList();
        }
    }
}
