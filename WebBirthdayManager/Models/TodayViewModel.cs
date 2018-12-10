using System.Collections.Generic;

namespace WebBirthdayManager.Models
{
    public class TodayViewModel
    {
        public string PageTitle { get; set; }
        public string PageHeader { get; set; }
        public IEnumerable<PersonViewModel> PeopleWithBirthdayToday { get; set; }
        public string NoBirthdaysTodayMessage { get; set; }
    }
}