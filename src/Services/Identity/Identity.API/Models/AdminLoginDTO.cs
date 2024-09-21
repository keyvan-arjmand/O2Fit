using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models
{
    public class AdminLoginDTO
    {
        [Required(ErrorMessage = "نام کاربری اجباری است")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "گذر واژه اجباری است")]
        public string Password { get; set; }
    }
}
