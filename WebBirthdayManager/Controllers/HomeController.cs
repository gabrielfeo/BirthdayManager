using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Validator;
using WebBirthdayManager.Models;
using static Repository.RepositoryFactory.StorageOption;

namespace WebBirthdayManager.Controllers
{
    public class HomeController : Controller
    {
        public IRepository<Person> Repository { get; private set; }

        public HomeController()
        {
            var personValidator = new ValidatorFactory().NewValidator<Person>();
            Repository = new RepositoryFactory().NewRepository(Filesystem, personValidator);
        }

        public IActionResult Index()
        {
            TodayViewModel viewModel = new TodayViewModel()
            {
                PeopleWithBirthdayToday = Repository.GetAll().ToImmutableList()
            };
            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}