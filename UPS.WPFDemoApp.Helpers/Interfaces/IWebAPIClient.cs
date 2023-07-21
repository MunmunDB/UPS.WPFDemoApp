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



    public interface IWebAPIClient
    {
        public EmployeeDetailsVM GetEmployeeDetailsVM();
        public Message UpdateEmployee(EmployeeVM employeeVM);
    }

  
}