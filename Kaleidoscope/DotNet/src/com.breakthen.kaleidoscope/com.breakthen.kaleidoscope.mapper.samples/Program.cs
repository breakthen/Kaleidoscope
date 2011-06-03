using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace com.breakthen.kaleidoscope.mapper.samples
{
    public class Program
    {
        static void Main(string[] args)
        {
            var personItem = new PersonItem {  Age = 25, FullName="Breakthen"};

            var person = Mapper.Map<PersonItem, Person>(personItem);

            Console.WriteLine("Person age: {0}, name: {1}.", person.Age, person.Name);
            Console.WriteLine("=========================+++++++============================");

            //IDataReader
            //IDataRecord

            TestDataReaderMapper();
            Console.WriteLine("=========================+++++++============================");
            
            //DataRow 
            TestDataRowMapper();
            Console.ReadKey();
        }

        static void TestDataReaderMapper()
        {
            var connectionString = "Data Source=(local);Initial Catalog=Frog;Integrated Security=True";
            string queryString = "SELECT Id, First_Name, Last_Name, Age FROM dbo.Person;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    var person = Mapper.Map<IDataReader, PersonDb>(reader);
                    Console.WriteLine(String.Format("{0}, {1}, {2}, {3}", person.Id, person.FirstName, person.LastName, person.Age));
                }

                // Call Close when done reading.
                reader.Close();
            }
        }

        static void TestDataRowMapper()
        {
            var connectionString = "Data Source=(local);Initial Catalog=Frog;Integrated Security=True";
            string queryString = "SELECT Id, First_Name, Last_Name, Age FROM dbo.Person;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                 //Create a SqlDataAdapter
                SqlDataAdapter adapter = new SqlDataAdapter();

                // Open the connection.
                connection.Open();

                // Create a SqlCommand
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = CommandType.Text;

                // Set the SqlDataAdapter's SelectCommand.
                adapter.SelectCommand = command;

                // Fill the DataSet.
                DataTable dt = new DataTable("Person");
                adapter.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    var person = Mapper.Map<DataRow, PersonDb>(row);
                    Console.WriteLine(String.Format("{0}, {1}, {2}, {3}", person.Id, person.FirstName, person.LastName, person.Age));
                }
            }
        }
    }

    class Person
    {
        public int Age { get; set; }

        [Mapping(Name = "FullName", Ignored = false)]
        public string Name { get; set; }

        public string Gender { get; set; }
    }

    class PersonDb
    {
        public int Age { get; set; }

        [Mapping(Name = "First_Name", Ignored = false)]
        public string FirstName { get; set; }

        [Mapping(Name = "Last_Name", Ignored = false)]
        public string LastName { get; set; }

        public int Id { get; set; }
        public int Gender { get; set; }
    }

    class PersonItem
    {
        public int Age { get; set; }

        public string FullName { get; set; }

    }
}
