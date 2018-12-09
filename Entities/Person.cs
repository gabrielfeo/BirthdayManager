using System;
using Newtonsoft.Json;

namespace Entities
{
    public class Person : IEquatable<Person>
    {
        public string Id { get; }
        public string Name { get; set; }
        public Birthday Birthday { get; }

        [JsonConstructor]
        public Person(string id, string name, Birthday birthday)
        {
            this.Id = id;
            this.Name = name;
            this.Birthday = birthday;
        }

        public bool Equals(Person other) => (other != null) && (this.HasId(other.Id));
        public bool HasId(string id) => this.Id.Equals(id);
    }
}