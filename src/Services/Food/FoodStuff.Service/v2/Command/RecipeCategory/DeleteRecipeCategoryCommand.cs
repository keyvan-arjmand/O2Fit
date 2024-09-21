using Amazon.Runtime.Internal;
using MediatR;

namespace FoodStuff.Service.v2.Command.RecipeCategory
{
    public class DeleteRecipeCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}