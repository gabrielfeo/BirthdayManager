using System.Collections.Generic;
using System.Linq;
using Entities;

namespace WebBirthdayManager.Models
{
    public class TodayViewModel
    {
        public IList<Person> PeopleWithBirthdayToday { get; set; }
    }
}