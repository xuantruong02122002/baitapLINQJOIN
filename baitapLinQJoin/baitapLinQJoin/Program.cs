using System;
using System.Collections.Generic;
using System.Linq;

namespace baitapLinQJoin
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public DateTime Birthday { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Tạo danh sách các phòng ban
            var departments = new List<Department>
        {
            new Department { Id = 1, Name = "XuanTruong", Address = "104 van don" },
            new Department { Id = 2, Name = "Mytam", Address = "22 khuc hao" },
            new Department { Id = 3, Name = "ThuCao", Address = "55 ngo quyen" }

        };

            // Tạo danh sách các nhân viên
            var employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "tin", Address = "141 tran hung dao", Age = 22, Salary = 50000, Birthday = new DateTime(2002, 2, 12) },
            new Employee { Id = 2, Name = "manh", Address = "33 le dinh ly", Age = 21, Salary = 60000, Birthday = new DateTime(2002, 10, 20) },
            new Employee { Id = 3, Name = "nam", Address = "12 an hai tay", Age = 25, Salary = 40000, Birthday = new DateTime(2002, 3, 25) }
        };

            //2a LINQ query để tìm max, min, average lương của nhân viên
            var maxSalary = employees.Max(e => e.Salary);
            var minSalary = employees.Min(e => e.Salary);
            var averageSalary = employees.Average(e => e.Salary);
            Console.WriteLine("2a ");
            Console.WriteLine($"Max Salary: {maxSalary}");
            Console.WriteLine($"Min Salary: {minSalary}");
            Console.WriteLine($"Average Salary: {averageSalary}");

            //2b LINQ Join
            Console.WriteLine("2b LINQ Join ");
            var joinQuery = from emp in employees
                            join dept in departments on emp.Id equals dept.Id into empDept
                            from ed in empDept.DefaultIfEmpty()
                            select new { EmployeeName = emp.Name, DepartmentName = ed == null ? "No Department" : ed.Name };

            foreach (var item in joinQuery)
            {
          
                Console.WriteLine($"Employee: {item.EmployeeName}, Department: {item.DepartmentName}");
            }

            //2b LINQ GroupJoin
            Console.WriteLine("2b LINQ GroupJoin ");
            var groupJoinQuery = from dept in departments
                                 join emp in employees on dept.Id equals emp.Id into empGroup
                                 select new { DepartmentName = dept.Name, Employees = empGroup };

            foreach (var item in groupJoinQuery)
            {
                Console.WriteLine($"Department: {item.DepartmentName}");
                foreach (var emp in item.Employees)
                {
                    Console.WriteLine($" - Employee: {emp.Name}");
                }
            }

            //2b LINQ Left Join
            Console.WriteLine("2b LINQ Left Join ");
            var leftJoinQuery = from dept in departments
                                join emp in employees on dept.Id equals emp.Id into empDept
                                from ed in empDept.DefaultIfEmpty()
                                select new { DepartmentName = dept.Name, EmployeeName = ed == null ? "No Employee" : ed.Name };

            foreach (var item in leftJoinQuery)
            {
                Console.WriteLine($"Department: {item.DepartmentName}, Employee: {item.EmployeeName}");
            }

            //2b LINQ Right Join
            Console.WriteLine("2b LINQ Right Join ");
            var rightJoinQuery = from emp in employees
                                 join dept in departments on emp.Id equals dept.Id into empDept
                                 from ed in empDept.DefaultIfEmpty()
                                 select new { DepartmentName = ed == null ? "No Department" : ed.Name, EmployeeName = emp.Name };

            foreach (var item in rightJoinQuery)
            {
                Console.WriteLine($"Department: {item.DepartmentName}, Employee: {item.EmployeeName}");
            }

            //2c Tìm max, min tuổi của nhân viên
            Console.WriteLine("2c ");
            var maxAge = employees.Max(e => e.Age);
            var minAge = employees.Min(e => e.Age);
            Console.WriteLine($"Max Age: {maxAge}");
            Console.WriteLine($"Min Age: {minAge}");
        }
    }
}
