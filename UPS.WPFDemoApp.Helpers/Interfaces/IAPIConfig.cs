using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPS.WPFDemoApp.Helpers.Interfaces
{
    public interface IAPIUrlConfig
    {
        public Uri GetUrl{ get; set; }
        public Uri PostUrl { get; set; }
        public Uri DeleteUrl { get; set; }
        public string accessToken { get; set; }
       
    }
}
