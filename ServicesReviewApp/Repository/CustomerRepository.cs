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
            this.context = context;
        }

        public bool CreateCustomer(Customer customer)
        {
            context.Add(customer);
            return Save();
        }

        public bool customerExist(int id)
        {
            return context.Customers.Any(c=>c.CustomerId == id);
        }


        public bool DeleteCustomer(Customer customer)
        {
            context.Remove(customer);
            return Save();
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

        public bool Save()
        { 
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCustomer(Customer customer)
        {
            context.Update(customer);
            return Save();
        }
    }
}
