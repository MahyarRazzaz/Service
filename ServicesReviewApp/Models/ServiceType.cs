namespace ServicesReviewApp.Models
{
    public class ServiceType
    {
        public int ServiceTypeId { get; set; }
        public string ServiceTypeTitle { get; set; }
        public ICollection<ServicesDetail> ServicesDetails { get; set; }
    }
}
