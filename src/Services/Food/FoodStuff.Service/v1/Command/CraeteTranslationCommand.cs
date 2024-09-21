using System;
using System.Collections.Generic;
using System.Text;
using FoodStuff.Domain.Entities.Translation;
using MediatR;

namespace Service.v1.Command
{
    public class CreateTranslationCommand : IRequest<Translation>
    {
        public Translation Translation { get; set; }
    }
}
