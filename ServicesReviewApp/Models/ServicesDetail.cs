namespace ServicesReviewApp.Models
{
    public class ServicesDetail
    {
        public int  ServicesDetailId { get; set; }
        public int ServiceId { get; set; }
        public Service  Service { get; set; }
        public int? PartId { get; set; }
        public Part Part { get; set; }
        public int Wage { get; set; }
        public int PartPrice { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }


    }
}
