using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPS.WPFDemoApp.Helpers.Interfaces;

namespace UPS.WPFDemoApp.Helpers
{
    public class APIUrlConfig : IAPIUrlConfig
    {
        public Uri GetUrl { get; set; }
        public Uri PostUrl { get; set; }
        public Uri DeleteUrl { get; set; }
        public string accessToken { get; set; }

        
    }
}
