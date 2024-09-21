﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.v1.Command
{
    public class DeleteTranslationCommand : IRequest
    {
        public List<int> Ids { get; set; }
    }
}
