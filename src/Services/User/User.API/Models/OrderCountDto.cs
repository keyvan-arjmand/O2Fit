namespace User.API.Models
{
    public class OrderCountDto
    {
        public int OrderCount { get; set; }
        public int OrderSuccessCount { get; set; }
        public int OrderUnSuccessCount { get; set; }
    }
}
