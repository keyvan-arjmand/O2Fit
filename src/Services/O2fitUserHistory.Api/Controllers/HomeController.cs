﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O2fitUserHistory.Api.Controllers
{
    public class HomeController : Controller
    {

        public async Task<IActionResult> Index()
        {
            return new RedirectResult("~/swagger");
        }

    }
}
