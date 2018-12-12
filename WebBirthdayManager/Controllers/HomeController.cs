using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Repository;
using Validator;
using WebBirthdayManager.Cache;
using WebBirthdayManager.Models;
using static Repository.RepositoryFactory.StorageOption;

namespace WebBirthdayManager.Controllers
{
    public class HomeController : Controller

    {
        private IRepository<Person> Repository { get; }
        private IMemoryCache Cache { get; }

        public HomeController(IMemoryCache cache)
        {
            var personValidator = new ValidatorFactory().NewValidator<Person>();
            Repository = new RepositoryFactory().NewRepository(RelationalDatabase, personValidator);
            this.Cache = cache;

            Cache.Set(CacheKeys.PersonRepository, Repository);
        }

        public IActionResult Index()
        {
            var peopleWithBirthdayToday = Repository.Search(DateTime.Today.ToString());

            PersonViewModel ModelToViewModel(Person person) => new PersonViewModel
            {
                Name = person.Name,
                Birthday = person.Birthday.GetNextDate().ToString("yyyy MMMM dd")
            };

            return View(new TodayViewModel
            {
                PageTitle = "Today",
                PageHeader = "Today's birthdays",
                PeopleWithBirthdayToday = peopleWithBirthdayToday.Select(ModelToViewModel),
                NoBirthdaysTodayMessage = "Looks like nothing's happening today."
            });
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