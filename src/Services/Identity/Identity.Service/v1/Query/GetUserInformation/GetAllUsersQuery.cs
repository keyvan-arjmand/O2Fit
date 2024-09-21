using System;
using Identity.Common.Utilities;
using MediatR;

namespace Identity.Service.v1.Query.GetUserInformation
{
    public class GetAllUsersQuery : IRequest<PageResult<GetAllUsersResult>>
    {
        public string UserName { get; set; }
        public int PageSize { get; set; }
        public int? Page { get; set; }
    }

    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberConfirmed { get; set; }
        public string Language { get; set; }
        public DateTime RegisterDate { get; set; }
        public string ImageUri { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
        public string ReferreralCode { get; set; }
        public string ReferreralInviter { get; set; }
        public string ReferreralCountBuy { get; set; }
    }

    public class GetAllUsersResult  
    {
        
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
        public string ReferreralCode { get; set; }
        public string ReferreralInviter { get; set; }
        public string ReferreralCountBuy { get; set; }

        public int Id { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ImageUri { get; set; }
        public DateTime? PkExpireDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public bool IsProfileComplete { get; set; }
        public string Market { get; set; }
        public string Brand { get; set; }
        public int Os { get; set; }

    }
}
