using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UPS.WPFDemoApp.ViewModels;
using UPS.WPFDemoApp.Helpers;
using System.Reflection;
using UPS.WPFDemoApp.Helpers;
using System.Data;
using Microsoft.Extensions.Logging;
using System.Net;

namespace UPS.WPFDemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        readonly IEmployeeHelper helper;
        IEmployeeVM _employeeVM;
        private readonly ILogger _logger;
        public EmployeeWindow( IEmployeeHelper employeeHelper, IEmployeeVM employeeVM, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("WpfDemoApp");
            helper = employeeHelper;
            _employeeVM = employeeVM;
            InitializeComponent();
            BindGrid();
            ResetWindow();
            SetCombobox();
            
        }

        WebAPIClient _client;
        /// <summary>
        /// Save Employee new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.recordToAddOrUpdate = this.DataContext as EmployeeVM;

                var returnresult = helper.UpdateEmployee();
                if (returnresult.isSuccess)
                {
                    MessageBox.Show(returnresult.message);
                    BindGrid();
                    ResetWindow();
                }
                else
                    MessageBox.Show("Error Code - " + returnresult.status + " : Message - " + returnresult.message);
            }
            catch (Exception ex) { _logger.LogError("MainWindow_Button_Click", ex.Message); }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dataRowEmp = (EmployeeVM)((Button)e.Source).DataContext;
                var returnresult = helper.RemoveEmployee(dataRowEmp.ID);
                if (returnresult.isSuccess)
                {
                    if (returnresult.status == HttpStatusCode.NoContent)
                        MessageBox.Show("Deleted");
                    else
                        MessageBox.Show(returnresult.message);
                   
                    BindGrid();
                    ResetWindow();
                }
                else
                    MessageBox.Show("Error Code - " + returnresult.status + " : Message - " + returnresult.message);
            }
            catch (Exception ex) { _logger.LogError("MainWindow_btnDelete_Click", ex.Message); }
        }

        /// <summary>
        /// Populate Employee Grid
        /// </summary>
        private void  BindGrid()
        {
            try
            {
                var returnresult = helper.GetAllEmployees();
            EmployeeGrid.ItemsSource = returnresult.employees;
            }
            catch (Exception ex) { _logger.LogError("MainWindow_BindGrid", ex.Message); }
        }

        /// <summary>
        /// Clear MainWindow
        /// </summary>
        private void ResetWindow()=> this.DataContext = _employeeVM;
        /// <summary>
        /// INitialise Combobox Gender
        /// </summary>
        private void SetCombobox()=> ComboboxGender.ItemsSource = typeof(BaseVM.GenderEnum).GetEnumNames();
    }
}
