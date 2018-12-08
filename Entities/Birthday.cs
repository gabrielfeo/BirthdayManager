using System;
					
namespace Entities
{
    public struct Birthday
    {
        public int Day { get; }
        public int Month { get; }

        public Birthday(int month, int day)
        {
            this.Month = month;
            this.Day = day;
        }

        public Birthday(DateTime date)
        {
            this.Month = date.Month;
            this.Day = date.Day;
        }
		
		public DateTime GetNextDate()
		{
            var today = DateTime.Now;
            var currentYearBirthday = new DateTime(today.Year, Month, Day);
            var yearOfNextBirthday = (today < currentYearBirthday) ? today.Year : today.Year+1;
            return new DateTime(yearOfNextBirthday, Month, Day);
        }
    }
}