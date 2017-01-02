using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncDelegate
{
    public class Employee
    {
        public string Post { get; set; }
        public int Salary { get; set; }

        public Employee GetEmployeeInfo()
        {
            try
            {
                Employee employee = new Employee();
                employee.Post = "开发";
                employee.Salary = 5000;

                return employee;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
