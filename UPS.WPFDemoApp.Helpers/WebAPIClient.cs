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

    public class WebAPIClient : IWebAPIClient
    {
        
        IEmployeeToEmployeeDetails empconverter;
        IHttpClientFactory _httpclientfactory;
                public WebAPIClient(IEmployeeToEmployeeDetails converter, IHttpClientFactory httpclientfactory) { empconverter = converter; _httpclientfactory =  httpclientfactory;  }
        /// <summary>
        /// Call to API to fetch all Employee list
        /// </summary>
        /// <returns></returns>
        public EmployeeDetailsVM GetEmployeeDetailsVM()
        {
            
            string token = string.Empty;
           var httpResponse = GetEmployeeAPICall();
            var employeeDetailsVm = PrepareEmployeeDetailsVM(httpResponse);
            
            return employeeDetailsVm;
        }
        /// <summary>
        /// Call to API to Save the new employee record to database
        /// </summary>
        /// <param name="employeeVM"></param>
        /// <returns></returns>
        public Message UpdateEmployee(EmployeeVM employeeVM)
        {
            var employee = empconverter.ConvertToEmployee(employeeVM);
            var returnresponse = CreateEmployeeAPICall(employee);
            return returnresponse;
        }

        Message CreateEmployeeAPICall(Employee employee)
        {
            HttpClient client = _httpclientfactory.CreateClient();
            client.BaseAddress = new Uri("https://gorest.co.in/public/v2/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync<Employee>("users?access-token=0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023", employee ).Result;

            return new Message() { isSuccess= response.IsSuccessStatusCode, message= Convert.ToString(response.ReasonPhrase), status = response.StatusCode  };

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
                int.TryParse(totalpagesstr.FirstOrDefault(), out int  totalpages);
                return new EmployeeDetailsVM() { nextPage = nextpagelinkstr.FirstOrDefault(), employees = empconverter.ConvertToEmployeeVM(emplist), pageCount = pagelimit, pageSize = pagelimit, prevPage = prevpagelinkstr.FirstOrDefault(), totalCount = totalcount };

            }
            
            return null;
        }

        HttpResponseMessage GetEmployeeAPICall()
        {
            IEnumerable<Employee> emplist;
            HttpClient client = _httpclientfactory.CreateClient();
            client.BaseAddress = new Uri("https://gorest.co.in/public/v2/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("users?access-token=0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023").Result;
            return response;
            
        }
    }


}