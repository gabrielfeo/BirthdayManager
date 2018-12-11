using WebBirthdayManager.Models.Birthdays.Forms;

namespace WebBirthdayManager.Models.Birthdays
{
    public class UpdateViewModel
    {
        public string PageTitle { get; set; }
        public string PageHeader { get; set; }

        public string NewNameInputFieldLabel { get; set; }
        public string SubmitButtonLabel { get; set; }
        public UpdateFormModel FormModel { get; set; }

        public string PersonIdToBeUpdated { get; set; }
        public PersonViewModel PersonToBeUpdated { get; set; }


    }
}