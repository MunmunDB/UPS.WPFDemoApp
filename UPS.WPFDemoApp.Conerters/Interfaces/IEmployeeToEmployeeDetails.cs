using UPS.WPFDemoApp.Models;
using UPS.WPFDemoApp.ViewModels;
using static UPS.WPFDemoApp.ViewModels.BaseVM;

namespace UPS.WPFDemoApp.Converters
{
    public interface IEmployeeToEmployeeDetails
    {

        public EmployeeDetailsVM ConvertToEmployeeDetailsVM(IEnumerable<Employee> employees);

        public Employee ConvertToEmployee(EmployeeVM employee);
        public IEnumerable<EmployeeVM> ConvertToEmployeeVM(IEnumerable<Employee> employees);
    }
}