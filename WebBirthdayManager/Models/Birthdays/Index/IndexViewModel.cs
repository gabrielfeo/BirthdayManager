using System.Collections.Generic;

namespace WebBirthdayManager.Models.Birthdays.Index
{
    public class IndexViewModel
    {
        public string PageTitle { get; set; }
        public string PageHeader { get; set; }
        public string NoPeopleMessage { get; set; }

        public IEnumerable<PersonViewModel> People { get; set; }
    }
}