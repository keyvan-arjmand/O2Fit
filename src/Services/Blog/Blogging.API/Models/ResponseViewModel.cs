using Blogging.Service.Models;
using Microsoft.AspNetCore.Http;

namespace Blogging.API.Models
{
    public class ResponseViewModel
    {
        public int Id { get; set; }
        public int NameId { get; set; }
        public TranslationNewDto ResponseTranslation { get; set; }
        public string ImageUrl { get; set; }
    }
}
