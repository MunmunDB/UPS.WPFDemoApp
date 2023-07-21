using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using UPS.WPFDemoApp.Helpers.Interfaces;

namespace UPS.WPFDemoApp.Helpers
{

    public class ServiceClient : IServiceClient
    {
        IHttpClientFactory _httpclientfactory;
        private readonly ILogger _logger;
        public Uri? serviceUri { get; set; }

        public string serviceToken { get; set; }
        const string mediaType = Utility.mediaType;
        public ServiceClient(IHttpClientFactory httpclientfactory, ILoggerFactory logger)
        {
            _httpclientfactory = httpclientfactory;
    }
        
       public HttpResponseMessage Get()
        {
            try
            {
                HttpClient client = _httpclientfactory.CreateClient();
                client.BaseAddress = this.serviceUri;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                HttpResponseMessage response = client.GetAsync("?" + serviceToken).Result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("ServiceClient_Get", ex.Message);
                return null;
            }
        }

        public HttpResponseMessage Post<T>(T content)
        {
            try
            {
                HttpClient client = _httpclientfactory.CreateClient();
                client.BaseAddress = this.serviceUri;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                HttpResponseMessage response = client.PostAsJsonAsync("?" + serviceToken, content).Result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("ServiceClient_Post<T>", ex.Message);
                return null;
            }
        }

        public HttpResponseMessage Delete(int Id)
        {
            try
            {
                HttpClient client = _httpclientfactory.CreateClient();
                var uristr = $"{serviceUri}/{Id}?{serviceToken}";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                HttpResponseMessage response = client.DeleteAsync(uristr).Result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("ServiceClient_Delete", ex.Message);
                return null;
            }
        }
    }


}