using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Validator;
using WebBirthdayManager.Models;
using WebBirthdayManager.Models.Birthdays;
using static Repository.RepositoryFactory.StorageOption;

namespace WebBirthdayManager.Controllers
{
    public class BirthdaysController : Controller
    {
        private IRepository<Person> Repository { get; }

        public BirthdaysController()
        {
            var personValidator = new ValidatorFactory().NewValidator<Person>();
            Repository = new RepositoryFactory().NewRepository(Filesystem, personValidator);
        }

        public IActionResult Index()
        {
            return View("Error");
        }

        public IActionResult Add()
        {
            return View("Error");
        }

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

        [HttpPatch]
        public IActionResult PostUpdate(string id, string newName)
        {
            bool personFound, updateSuccessful = false;
            
            var personToBeUpdated = Repository.GetById(id);
            personFound = (personToBeUpdated != null);
            if (personFound)
            {
                personToBeUpdated.Name = newName;
                Repository.Update(personToBeUpdated, out updateSuccessful);
            }

            if (updateSuccessful) return new OkResult();
            else if (!personFound) return new NotFoundResult();
            else return new BadRequestResult();
        }

        public IActionResult Delete(string id)
        {
            return View("Error");
        }
    }
}