namespace WebBirthdayManager.Models.Birthdays.Add
{
    public class AddViewModel
    {
        public string PageTitle { get; set; }
        public string PageHeader { get; set; }

        public string NameInputFieldLabel { get; set; }
        public string BirthdayInputFieldLabel { get; set; }
        public string SubmitButtonLabel { get; set; }
        public AddFormModel FormModel { get; set; }
    }
}