using Entities;
using Validator;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository.PersonNs
{
    internal class DbPersonRepository : PersonRepository
    {

        private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gabriel.feo\Desktop\BirthdayManager\Repository\App_Data\Database1.mdf;Integrated Security=True";

        public DbPersonRepository(IValidator<Person> personValidator) : base(personValidator)
        {
        }

        public override Person Get(Person person)
        {
            return GetById(person.Id);
        }

        public override Person GetById(string personId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = $"SELECT * FROM People WHERE Id=\'{personId}\'";
                var selectCommand = new SqlCommand(query, connection);
                var people = new List<Person>();

                connection.Open();
                using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        var person = ReadPersonFrom(reader);
                        people.Add(person);
                    }
                }
                return people.First();
            }
        }

        public override IEnumerable<Person> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM People";
                var selectCommand = new SqlCommand(query, connection);
                var people = new List<Person>();

                connection.Open();
                using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        var person = ReadPersonFrom(reader);
                        people.Add(person);
                    }
                }
                return people;
            }
        }

        public override void Insert(Person newPerson, out bool successful)
        {
            successful = false;
            if (!CanInsert(newPerson)) return;

            using (var connection = new SqlConnection(connectionString))
            {
                var formattedBirthday = newPerson.Birthday.GetNextDate().ToString();
                var query = $"INSERT INTO People (Id, Name, Birthday) VALUES (\'{newPerson.Id}\', \'{newPerson.Name}\', \'{formattedBirthday}\')";
                var insertCommand = new SqlCommand(query, connection);

                connection.Open();
                var rowsAffected = insertCommand.ExecuteNonQuery();
                if (rowsAffected >= 1) successful = true;
            }
        }

        public override void Update(Person changedPerson, out bool successful)
        {
            successful = false;
            if (!CanUpdate(changedPerson)) return;

            using (var connection = new SqlConnection(connectionString))
            {
                var query = $"UPDATE People SET Name=\'{changedPerson.Name}\' WHERE Id=\'{changedPerson.Id}\'";
                var updateCommand = new SqlCommand(query, connection);

                connection.Open();
                var rowsAffected = updateCommand.ExecuteNonQuery();
                if (rowsAffected >= 1) successful = true;
            }
        }
        public override void Delete(string personId, out bool successful)
        {
            successful = false;
            if (!CanDelete(personId)) return;

            using (var connection = new SqlConnection(connectionString))
            {
                var query = $"DELETE FROM People WHERE Id=\'{personId}\'";
                var deleteCommand = new SqlCommand(query, connection);

                connection.Open();
                var rowsAffected = deleteCommand.ExecuteNonQuery();
                if (rowsAffected >= 1) successful = true;
            }
        }

        private Person ReadPersonFrom(SqlDataReader reader)
        {
            var id = (string)reader["Id"];
            var name = (string)reader["Name"];
            var birthdayDateTime = (DateTime)reader["Birthday"];
            return new Person(id, name, new Birthday(birthdayDateTime));
        }
    }
}
