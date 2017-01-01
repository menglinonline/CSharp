using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 1, 3, 4, 5, 7 };
            var query = from num in numbers
                        where num % 2 == 1 || num % 3 == 1
                        orderby num ascending
                        select num;
            foreach (var i in query)
            {
                Console.WriteLine(i);
            }

            //group
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer() { Name = "David", City = "Beijing"});
            customers.Add(new Customer() { Name = "Jack", City = "Beijing", OtherName = "jieke" });
            customers.Add(new Customer() { Name = "Bruce", City = "Xian" });

            var queryCustomer = from c in customers
                                group c by c.City;
            foreach (var c in queryCustomer)
            {
                Console.WriteLine(c.Key);
                foreach (var i in c)
                {
                    Console.WriteLine(" {0}", i.Name);
                }
            }

            //join
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee() { Name = "David", Id = 101 });
            employees.Add(new Employee() { Name = "jieke", Id = 102 });
            employees.Add(new Employee() { Name = "Mike", Id = 103 });

            var queryEmployee = from c in customers
                                join e in employees 
                                    on c.Name equals e.Name
                                select new { PersonName = c.Name, PersonId = e.Id, PersonCity = c.City };


            foreach (var p in queryEmployee)
            {
                Console.WriteLine("{0} {1} {2}", p.PersonId,p.PersonName,p.PersonCity);
            }
            Console.ReadLine();
        }
    }

    class Customer
    {
        public string Name
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }
        public string OtherName
        {
            get;
            set;
        }

    }

    class Employee
    {
        public string Name
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public string OtherName
        {
            get;
            set;
        }
    }
}
