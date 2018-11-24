using System;

namespace Entities
{
    public class Person : IEquatable<Person>
    {
        public Guid id { get; }
        public string Name { get; set; }
        public Birthday Birthday { get; }

        public Person(string name, Birthday birthday)
        {
            this.id = Guid.NewGuid();
            this.Name = name;
            this.Birthday = birthday;
        } 

        public bool Equals(Person other) => this.HasId(other.id);
        public bool HasId(Guid id) => this.id.Equals(id);
    }
}