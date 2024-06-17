using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SQL_PRJ
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private DataManager manager;
        private ListBox employeesListBox;
        private List<Employees> cachedEmployees;
        private List<Employees> filteredEmployeesList;
        Database database = new Database();
        public Window1(DataManager dataManager, ListBox employeeListBox)
        {
            InitializeComponent();
            this.manager = dataManager;
            this.employeesListBox = employeeListBox;
            LoadEmployees();
        }
        /*private void ReloadList()
        {
            var employees = manager.ReloadEmployeeList();

            employeesListBox.Items.Clear();
            employees.ForEach(employee => employeesListBox.Items.Add(employee));

        }*/

        private void LoadEmployees()
        {
            cachedEmployees = manager.ReloadEmployeeList();
            filteredEmployeesList = new List<Employees>(cachedEmployees);
            DisplayEmployees(filteredEmployeesList);
        }
        private void LoadEmployeesWSales()
        {
            employeesListBox.Items.Clear();
            var employees = manager.ReloadEmployeesWSales();
            employees.ForEach(employee => employeesListBox.Items.Add(employee));

        }
        private void DisplayEmployees(List<Employees> employees)
        {
            employeesListBox.Items.Clear();
            foreach (var employee in employees)
            {
                employeesListBox.Items.Add(employee);
            }
        }
        private void Search_Employee_By_Name_Click(object sender, RoutedEventArgs e)
        {
            string givenName = textBoxSearchGivenName.Text;
            string familyName = textBoxSearchFamilyName.Text;

            filteredEmployeesList = database.SearchByNameInDB(givenName, familyName);
            DisplayEmployees(filteredEmployeesList);
        }

        private void Search_By_Salary_Click(object sender, RoutedEventArgs e)
        {
            float minSalary;
            if (!float.TryParse(textBoxMinSalary.Text, out minSalary))
            {
                MessageBox.Show("Please enter a number");
            }
            float maxSalary;
            if (!float.TryParse(textBoxMaxSalary.Text, out maxSalary))
            {
                MessageBox.Show("Please enter a number");
            }

            filteredEmployeesList = database.SearchBySalaryInDB(minSalary, maxSalary);
            DisplayEmployees(filteredEmployeesList);

        }
        private void Search_By_Branch_Click(object sender, RoutedEventArgs e) //TODO maybe?: need to implement branch name searching
        {
            int branchID;
            if (!int.TryParse(textBoxBranchID.Text, out branchID))
            {
                MessageBox.Show("Please enter a number");
            }
            filteredEmployeesList = database.SearchByBranchInDB(branchID);
            DisplayEmployees(filteredEmployeesList);
        }


        private void Show_All_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }

        private void Filter_By_Click(object sender, RoutedEventArgs e)
        {

            employeesListBox.Items.Clear();

            foreach (var employee in filteredEmployeesList)
            {
                string displayInfo = "";

                if (checkBoxFirstName.IsChecked == true)
                {
                    displayInfo += employee.FirstName + "  ";
                }
                if (checkBoxLastName.IsChecked == true)
                {
                    displayInfo += employee.LastName + "  ";
                }
                if (checkBoxDOB.IsChecked == true)
                {
                    displayInfo += employee.DateOfBirth.ToString("dd-MM-YY") + " | ";
                }
                if (checkBoxGenID.IsChecked == true)
                {
                    displayInfo += employee.GenderIdentity + " | ";
                }
                if (checkBoxSalary.IsChecked == true)
                {
                    displayInfo += "$" + employee.GrossSalary.ToString() + " | ";
                }
                if (checkBoxBranchID.IsChecked == true)
                {
                    displayInfo += "BranchID: " + employee.BranchID.ToString() + " | ";
                }
                if (checkBoxEmployeeID.IsChecked == true)
                {
                    displayInfo += "EmployeeID: " + employee.EmployeeID.ToString() + " | ";
                }
                if (checkBoxSupervisorID.IsChecked == true)
                {
                    displayInfo += "SupervisorID: " + employee.SupervisorID.ToString() + " | ";
                }

                employeesListBox.Items.Add(displayInfo.Trim());
            }
        }

        private void Total_Sales_By_ID_Click(object sender, RoutedEventArgs e)
        {
            int employeeID;
            if(!int.TryParse(textBoxEmployeeID.Text, out employeeID))
            {
                MessageBox.Show("Please enter a number");
            }
            List<SearchEmployees> filteredEmployees = database.SearchSalesByID(employeeID);
            employeesListBox.Items.Clear();
            foreach (SearchEmployees employees in filteredEmployees)
            {
                employeesListBox.Items.Add(employees);
            }

        }

        private void Total_Sales_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployeesWSales();
        }
    }

}
