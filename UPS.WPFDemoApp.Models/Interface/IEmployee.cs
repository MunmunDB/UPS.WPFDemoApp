﻿namespace UPS.WPFDemoApp.Models
{
    public interface IEmployee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string status { get; set; }

    }
    
}