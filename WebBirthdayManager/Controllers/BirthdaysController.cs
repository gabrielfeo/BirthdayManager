using System;
using System.Linq;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Repository;
using WebBirthdayManager.Cache;
using WebBirthdayManager.Extensions;
using WebBirthdayManager.Models;
using WebBirthdayManager.Models.Birthdays.Add;
using WebBirthdayManager.Models.Birthdays.Index;
using WebBirthdayManager.Models.Birthdays.Update;

namespace WebBirthdayManager.Controllers
{
    public class BirthdaysController : Controller
    {
        private IRepository<Person> Repository { get; }
        private IMemoryCache Cache { get; }

        public BirthdaysController(IMemoryCache cache)
        {
            this.Cache = cache;
            Repository = Cache.Get<IRepository<Person>>(CacheKeys.PersonRepository);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var personViewModels = Repository.GetAll().Select(person => new PersonViewModel
            {
                Name = person.Name,
                Birthday = person.Birthday.GetNextDate().ToString("yyyy MMMM dd"),
                Id = person.Id
            });
            
            return View(model: new IndexViewModel
            {
                PageTitle = "Birthdays",
                PageHeader = "All Birthdays",
                NoPeopleMessage = "There\'s no one here yet. Please add somebody\'s birthday to get started.",
                People = personViewModels
            });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(model: new AddViewModel
            {
                PageTitle = "Add Birthday",
                PageHeader = "Add Birthday",
                NameInputFieldLabel = "Name",
                BirthdayInputFieldLabel = "Birthday",
                SubmitButtonLabel = "Add",
            });
        }

        [HttpPost]
        public IActionResult Add(AddFormModel formModel)
        {
            var birthday = new Birthday(formModel.BirthdayDateTime);
            var person = new Person(Guid.NewGuid().ToString(), formModel.Name, birthday);
            
            Repository.Insert(person, out bool addWasSuccessful);

            if (addWasSuccessful) return Ok();
            return BadRequest();
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var personToBeUpdated = Repository.GetById(id);
            if (personToBeUpdated is null) return View("Error");

            var personViewModel = new PersonViewModel
            {
                Name = personToBeUpdated.Name,
                Birthday = personToBeUpdated.Birthday.GetNextDate().ToShortDateString()
            };

            return View(model: new UpdateViewModel
            {
                PageTitle = "Update Birthday",
                PageHeader = "Update Birthday",
                NewNameInputFieldLabel = "New name",
                SubmitButtonLabel = "Update",
                PersonToBeUpdated = personViewModel
            });
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateFormModel formModel)
        {
            bool updateSuccessful = false;

            var personToBeUpdated = Repository.GetById(id);
            bool personFound = (personToBeUpdated != null);

            if (personFound && formModel.NewName.IsNotEmpty())
            {
                personToBeUpdated.Name = formModel.NewName;
                Repository.Update(personToBeUpdated, out updateSuccessful);
            }

            if (updateSuccessful) return Ok();
            else if (!personFound) return NotFound();
            else return BadRequest();
        }

        public IActionResult Delete(string id)
        {
            return View("Error");
        }
    }
}