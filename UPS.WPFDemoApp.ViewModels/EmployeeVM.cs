using System.ComponentModel;
using System.Windows.Input;
using UPS.WPFDemoApp.Models;



namespace UPS.WPFDemoApp.ViewModels
{
    public class EmployeeVM : BaseVM , IEmployeeVM
    {
        Employee _employee;

        private ICommand _deleteCommand;
        public EmployeeVM()
        {
            _employee = new Employee();
        }

        public EmployeeVM(Employee employee) => _employee = employee;

        public int ID { get { return _employee.id; } set { _employee.id = value; OnPropertyChanged("ID"); } }
        public string Name { get { return _employee.name; } set { _employee.name = value; OnPropertyChanged("Name"); } }
        public string Email { get { return _employee.email; } set { _employee.email = value; OnPropertyChanged("Email"); } }

        //public GenderEnum? Gender { get { return (GenderEnum)Enum.Parse(typeof(GenderEnum), _employee.gender, true); } set { _employee.gender = Convert.ToString(value); } }
        public string Gender { get { return _employee.gender; } set { _employee.gender = Convert.ToString(value); OnPropertyChanged("Gender"); } }
        public bool Status { get { return _employee.status == StatusEnum.active.ToString(); } set { _employee.status = (value == true ? StatusEnum.active.ToString(): StatusEnum.inactive.ToString()); OnPropertyChanged("Status"); } }

    }

    public class EmployeeDetailsVM : BaseVM, IEmployeeDetailsVM
    {
        public EmployeeDetailsVM() { }
        public IEnumerable<EmployeeVM> employees { get; set; }

        public string? nextPage { get; set; }
        public string prevPage {  get; set; }

        public int pageSize { get; set; } 
        public int pageCount { get; set; }
        public int totalCount { get; set; }

    }
}