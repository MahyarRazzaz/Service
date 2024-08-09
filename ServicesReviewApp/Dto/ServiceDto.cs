using ServicesReviewApp.Models;

namespace ServicesReviewApp.Dto
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public int ServiceNumber { get; set; }
        public string ServiceTitle { get; set; }
        public DateTime ServiceDate { get; set; }
        public int Wage { get; set; }
        //?    
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public ICollection<ServicesDetail> ServicesDetails { get; set; }
        
       
    }
}
