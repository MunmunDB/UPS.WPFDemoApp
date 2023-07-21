using System.Net.Http.Headers;
using UPS.WPFDemoApp.Models;
using UPS.WPFDemoApp.Converters;
using UPS.WPFDemoApp.ViewModels;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using UPS.WPFDemoApp.Helpers.Interfaces;
using Microsoft.Extensions.Logging;

namespace UPS.WPFDemoApp.Helpers
{
    public class EmployeeHelper : IEmployeeHelper
    {
        
        IServiceClient aServiceClient;
        IEmployeeToEmployeeDetails empconverter;
        IEmployeeVM _employeeVM;
        private readonly ILogger _logger;
        public EmployeeHelper(IServiceClient serviceClient, IConfiguration configuration, IEmployeeToEmployeeDetails employeeToEmployeeDetails, IEmployeeVM employeeVM, ILoggerFactory loggerFactory) {
            _logger = loggerFactory.CreateLogger("WpfDemoApp_EmployeeHelper");
            serviceClient.serviceToken = configuration["access-token"];
            serviceClient.serviceUri= new Uri(configuration["serviceEmpUrl"]);     
            aServiceClient = serviceClient;
            empconverter = employeeToEmployeeDetails;
            _employeeVM = employeeVM;
        }

        public EmployeeVM recordToAddOrUpdate { set { _employeeVM = value; } }

        public EmployeeDetailsVM GetAllEmployees()
        {
            try
            {
                var httpResponse = aServiceClient.Get();
                var employeeDetailsVm = PrepareEmployeeDetailsVM(httpResponse);

                return employeeDetailsVm;
            }
            catch (Exception ex) { _logger.LogError("EployeeHelper_GetAllEmployees", ex.Message);  }
            return null;
        }

        public Message RemoveEmployee(int Id)
        {
            try
            {
                var httpResponse = aServiceClient.Delete(Id);
                return new Message() { isSuccess = httpResponse.IsSuccessStatusCode, message = Convert.ToString(httpResponse.ReasonPhrase), status = httpResponse.StatusCode };
            }
            catch (Exception ex) { _logger.LogError("EployeeHelper_RemoveEmployee", ex.Message); }
            return new Message() { isSuccess = false, message = "Exception Occurred" };
        }

        public Message UpdateEmployee()
        {
            try
            {
                var employeeobj = empconverter.ConvertToEmployee(_employeeVM as EmployeeVM);
           
            var httpResponse = aServiceClient.Post(employeeobj);
            

            return new Message() { isSuccess = httpResponse.IsSuccessStatusCode, message = Convert.ToString(httpResponse.ReasonPhrase), status = httpResponse.StatusCode };
            }
            catch (Exception ex) { _logger.LogError("EployeeHelper_UpdateEmployee", ex.Message); }
            return new Message() { isSuccess = false, message = "Exception Occurred" };
        }

        EmployeeDetailsVM PrepareEmployeeDetailsVM(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var emplist = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
                response.Headers.TryGetValues("X-PAGINATION-LIMIT", out var pagelimitstr);
                response.Headers.TryGetValues("X-PAGINATION-TOTAL", out var totalcountstr);
                response.Headers.TryGetValues("X-PAGINATION-PAGES", out var totalpagesstr);
                response.Headers.TryGetValues("X-LINKS-NEXT", out var nextpagelinkstr);
                response.Headers.TryGetValues("X-LINKS-PREVIOUS", out var prevpagelinkstr);
                int.TryParse(pagelimitstr.FirstOrDefault(), out int pagelimit);
                int.TryParse(totalcountstr.FirstOrDefault(), out int totalcount);
                int.TryParse(totalpagesstr.FirstOrDefault(), out int totalpages);
                return new EmployeeDetailsVM() { nextPage = nextpagelinkstr.FirstOrDefault(), employees = empconverter.ConvertToEmployeeVM(emplist), pageCount = totalpages, pageSize = pagelimit, prevPage = prevpagelinkstr.FirstOrDefault(), totalCount = totalcount };

            }

            return null;
        }
       
    }
}