using System.Collections.Generic;

namespace Blogging.Service.Models
{
    public class QuestionViewModel
    {
        public QuestionPutDto FrequentlyQuestion { get; set; }
        public List<ResponseViewModel> Responses { get; set; }
    }
}
