using System.Reflection.Metadata.Ecma335;
using UPS.WPFDemoApp.Models;
using UPS.WPFDemoApp.ViewModels;
using static UPS.WPFDemoApp.ViewModels.BaseVM;

namespace UPS.WPFDemoApp.Converters
{
    public class EmployeeToEmployeeDetails : IEmployeeToEmployeeDetails
    {

        public EmployeeDetailsVM ConvertToEmployeeDetailsVM(IEnumerable<Employee> employees)
        {
            var returnlist = from emp in employees
                             select new EmployeeVM(emp);
            return new EmployeeDetailsVM() { employees = returnlist };

        }
        public Employee ConvertToEmployee(EmployeeVM employee)
        {
            return new Employee(employee.ID, employee.Name, employee.Email, employee.Gender.ToString(), employee.Status ? "active" : "inactive");

        }
        /// <summary>
        /// Prepares a collection of EmployeeViewModel using the collection Model Employee
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        public IEnumerable<EmployeeVM> ConvertToEmployeeVM(IEnumerable<Employee> employees)
        {
            IEnumerable<EmployeeVM> returnlist;
            returnlist =   from emp in employees
                           select new EmployeeVM(emp);
            return returnlist;
        }

    }
}