namespace Identity.API.Models
{
    public class ConfirmCodeViewModel
    {
        public bool IsActive { get; set; }
        public bool WrongCode { get; set; }
        public bool ExpireCode { get; set; }

        public UserSelectViewModel UserSelectViewModel { get; set; }
    }
}
