using System;
using System.Collections.Generic;


namespace Identity.API.Models
{
    public class GetRegisterStatusUsersDTO
    {
        public DateTime RegisterDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public UserProfileInfoViewModelResult UserProfileInfoViewModelResult { get; set; }


    }
    public class GetRegisterStatusUsersViewModel
    {
        public List<GetRegisterStatusUsersDTO> GetRegisterStatusUsersDTO { get; set; }
        public int ActiveCount { get; set; }
        public int RegisterCount { get; set; }

    }

}
