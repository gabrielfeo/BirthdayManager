using System;

namespace Entities
{
    public class Person : IEquatable<Person>
    {
        public string id { get; }
        public string Name { get; set; }
        public Birthday Birthday { get; }

        public Person(string id, string name, Birthday birthday)
        {
            this.id = id;
            this.Name = name;
            this.Birthday = birthday;
        } 

        public bool Equals(Person other) => this.HasId(other.id);
        public bool HasId(string id) => this.id.Equals(id);
    }
}