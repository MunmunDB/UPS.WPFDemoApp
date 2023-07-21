using System.Net.Http.Headers;
using UPS.WPFDemoApp.Models;
using UPS.WPFDemoApp.Converters;
using UPS.WPFDemoApp.ViewModels;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Net.Http.Json;

namespace UPS.WPFDemoApp.Helpers
{
    public interface IEmployeeHelper
    {
        public EmployeeDetailsVM GetAllEmployees();
        public Message UpdateEmployee();
        public Message RemoveEmployee(int Id);

        public EmployeeVM recordToAddOrUpdate { set; }

    }
}