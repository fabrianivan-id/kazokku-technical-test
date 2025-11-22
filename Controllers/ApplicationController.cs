using System;
using DmsCreditScoring.Models;
using DmsCreditScoring.Services;
using Microsoft.AspNetCore.Mvc;

namespace DmsCreditScoring.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IScoringService _scoringService;

        public ApplicationController(IScoringService scoringService)
        {
            _scoringService = scoringService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new ApplicationForm
            {
                BirthDate = DateTime.Today.AddYears(-30),
                TenorYears = 10,
                NumberOfDependants = 0
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ApplicationForm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _scoringService.Calculate(model);
            return View("Result", result);
        }
    }
}
