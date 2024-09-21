using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query.GetMeasureUnitByVersion
{
    public class GetMeasureUnitByVersionQurey:IRequest<MeasureUnitsAndVersionSelectDTO>
    {
        public double Version { get; set; }
    }
}
