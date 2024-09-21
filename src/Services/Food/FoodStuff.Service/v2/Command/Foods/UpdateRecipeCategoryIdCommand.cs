using Amazon.Runtime.Internal;
using MediatR;

namespace FoodStuff.Service.v2.Command.Foods
{
    public class UpdateRecipeCategoryIdCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public int RecipeCategoryIdId { get; set; }
    }
}