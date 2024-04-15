namespace ServicesReviewApp.Models
{
    public class Part
    {
        public int PartId { get; set; }
        public string PartTitle { get; set; }
        public ICollection<ServicesDetail> servicesDetails { get; set; }
    }
}
