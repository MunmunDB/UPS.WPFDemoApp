namespace UPS.WPFDemoApp.Models
{

    public class Employee : IEmployee
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string gender { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public Employee(int id, string name, string email, string gender, string status)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.gender = gender;
            this.status = status;
        }

        public Employee()
        {
        }
    }
}