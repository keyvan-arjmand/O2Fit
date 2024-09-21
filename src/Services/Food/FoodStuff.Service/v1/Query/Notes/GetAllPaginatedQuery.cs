using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v1.Query.Notes
{
    public class GetAllPaginatedWithUserIdQuery : IRequest<PageResult<NoteDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int UserId { get; set; }
    }
}