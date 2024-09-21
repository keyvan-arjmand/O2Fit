namespace Blogging.API.Models
{
    public class GetByCategoryId
    {
        public int Id { get; set; }
        public int? page { get; set; }
        public int pageSize { get; set; }

    }
}
