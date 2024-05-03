using ServicesReviewApp.Data;
using ServicesReviewApp.Models;

namespace ServicesReviewApp.Repository
{
    public class CustomerRepository
    {
        private readonly DataContext context;

        public CustomerRepository(DataContext context)
        {
            this.context = context;
        }
        public ICollection<Customer>GetCustomers()
        {
            return context.Customers.OrderBy(c=>c.CustomerId).ToList();
        }
    }
}
