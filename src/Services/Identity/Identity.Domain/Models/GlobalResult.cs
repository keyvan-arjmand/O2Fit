namespace Identity.Domain.Models
{
    public class GlobalResult
    {
        public object data { get; set; }
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
    }


}
