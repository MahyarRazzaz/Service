using Microsoft.EntityFrameworkCore;
using ServicesReviewApp.Models;

namespace ServicesReviewApp.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options)
        {
            
        }

      

     
        public DbSet<Car> Cars { get; set; } 
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Part> parts { get; set; } 
        public DbSet<Service> Services { get; set; }
        public DbSet<ServicesDetail> ServicesDetails { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<ServicesDetail>()
                .HasKey(Se => new { Se.ServicesDetailId });
            modelBuilder.Entity<ServicesDetail>()
                .HasOne(s => s.Service)
                .WithMany(Se => Se.ServicesDetails)
                .HasForeignKey(e => e.ServiceId);
            modelBuilder.Entity<ServicesDetail>()
                .HasOne(s => s.Part)
                .WithMany(Se => Se.servicesDetails)
                .HasForeignKey(e => e.PartId);


            modelBuilder.Entity<Service>()
                .HasKey(Se => new {Se.ServiceId});
            modelBuilder.Entity<Service>()
                .HasOne(s => s.Customer)
                .WithMany(Se => Se.services)
                .HasForeignKey(e => e.CustomerId);
            modelBuilder.Entity<Service>()
                .HasOne(s => s.Car)
                .WithMany(Se => Se.Services)
                .HasForeignKey(e => e.CarId);
        }
        
    }
}
