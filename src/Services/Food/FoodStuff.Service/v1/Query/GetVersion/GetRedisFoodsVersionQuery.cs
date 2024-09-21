using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query.GetVersion
{
   public class GetRedisFoodsVersionQuery:IRequest<FoodsVersionModel>
    {
        public double lastVersion;
    }
}
