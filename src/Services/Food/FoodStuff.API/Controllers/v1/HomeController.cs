using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Contracts;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.API.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1")]
    public class HomeController : Controller
    {

        public async Task<IActionResult> Index()
        {
            return new RedirectResult("~/swagger");
        }
      
    }
}
