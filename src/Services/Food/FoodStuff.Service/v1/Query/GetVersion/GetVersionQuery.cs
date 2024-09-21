using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query.GetVersion
{
    public class GetVersionQuery:IRequest<GetVersionQueryResult>
    {
       public double lastVersion;
    }
}
