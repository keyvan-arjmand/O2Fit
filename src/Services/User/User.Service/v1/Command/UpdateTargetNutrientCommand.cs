using Domain;
using MediatR;
using System.Collections.Generic;

namespace User.Service.v1.Command
{
    public class UpdateTargetNutrientCommand : IRequest<UserProfile>
    {
        public int UserId { get; set; }
        public List<double> TargetNutrient { get; set; }
    }
}