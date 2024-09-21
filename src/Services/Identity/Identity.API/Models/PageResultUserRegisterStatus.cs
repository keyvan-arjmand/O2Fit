using Identity.Common.Utilities;

namespace Identity.API.Models
{
    public class PageResultUserRegisterStatus<T> : PageResult<T>
    {
        public int ActiveCount { get; set; }
        public int RegisterCount { get; set; }

    }
}
