using System;

namespace Entities
{
    public struct Person
    {
        public string Name { get; set; }
        public Birthday Birthday { get; }

        public Person(string name, Birthday birthday)
        {
            this.Name = name;
            this.Birthday = birthday;
        } 
    }
}