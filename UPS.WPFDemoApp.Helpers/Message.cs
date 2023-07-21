using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UPS.WPFDemoApp.Helpers
{

    public class Message: IMessage
    {
        public string message { get; set; }
        public HttpStatusCode status { get; set; }
        public bool isSuccess { get; set; }
    }
}
