namespace ServicesReviewApp.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public int ServiceNumber { get; set; }
        public string ServiceTitle{ get; set; }
        public DateTime ServiceDate { get; set; }
        public int Wage { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int CarId { get; set; }
         public Car Car { get; set; }
        public ICollection<ServicesDetail> ServicesDetails { get; set; }
    }
}
