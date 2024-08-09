using ServicesReviewApp.Models;

namespace ServicesReviewApp.Dto
{
    public class ServiceDetailDto
    {
        public int ServicesDetailId { get; set; }
        public int ServiceId { get; set; }
        public int? PartId { get; set; }
        public int Wage { get; set; }
        public int PartPrice { get; set; }
        public int ServiceTypeId { get; set; }
        
    }
}
