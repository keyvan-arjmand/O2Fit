using Amazon.Runtime.Internal;
using Common;
using MediatR;

namespace FoodStuff.Service.v1.Command.IngredientAllergies
{
    public class CreateIngredientAllergiesCommand : IRequest<Unit>
    {
        public int IngredientId { get; set; }
        public bool IsChecked { get; set; }
    }
}