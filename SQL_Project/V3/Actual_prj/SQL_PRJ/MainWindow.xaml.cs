using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQL_PRJ
{
    
    public partial class MainWindow : Window
    {
        DataManager manager = new DataManager();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ReloadList()
        {
            var employees = manager.ReloadEmployeeList();

            EmployeesList.Items.Clear();
            employees.ForEach(employee => EmployeesList.Items.Add(employee));

        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            ReloadList();

        }

        private void EmployeesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employees selected = EmployeesList.SelectedItem as Employees;
            if (selected != null)
            {
                textBoxGivenName.Text = selected.FirstName;
                textBoxFamilyName.Text = selected.LastName;
                textBoxDOB.Text = selected.DateOfBirth.ToString("dd-MM-yyyy");
                textBoxGenID.Text = selected.GenderIdentity;
                textBoxSupervisor.Text = selected.SupervisorID.ToString();
                textBoxBranch.Text = selected.BranchID.ToString();
                textBoxSalary.Text = selected.GrossSalary.ToString();
            }
        }

        private void EmployeeCreator(object sender, RoutedEventArgs e)
        {
            try
            {
                string givenName = textBoxGivenName.Text;
                string familyName = textBoxFamilyName.Text;
                DateTime dob = DateTime.Parse(textBoxDOB.Text);
                string genderIdentity = textBoxGenID.Text;
                int supervisorID = int.Parse(textBoxSupervisor.Text);
                int branchID = int.Parse(textBoxBranch.Text);
                int salary = int.Parse(textBoxSalary.Text);

                Employees newEmployee = manager.CreateNewEmployee(givenName, familyName, dob, genderIdentity, supervisorID, branchID, salary);


                MessageBox.Show("Employee added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding employee: {ex.Message}");
            }
            ReloadList();
        }

        private void EmployeeUpdater(object sender, RoutedEventArgs e)
        {

            if (EmployeesList.SelectedItem is Employees selectedEmployee)
            {
                try
                {
                    selectedEmployee.FirstName = textBoxGivenName.Text;
                    selectedEmployee.LastName = textBoxFamilyName.Text;
                    selectedEmployee.DateOfBirth = DateTime.Parse(textBoxDOB.Text);
                    selectedEmployee.GenderIdentity = textBoxGenID.Text;
                    selectedEmployee.SupervisorID = int.Parse(textBoxSupervisor.Text);
                    selectedEmployee.BranchID = int.Parse(textBoxBranch.Text);
                    selectedEmployee.GrossSalary = int.Parse(textBoxSalary.Text);

                    manager.UpdateEmployeeInList(selectedEmployee);


                    ReloadList();

                    MessageBox.Show("Employee updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating employee: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("No employee selected!");
            }
        }

        private void EmployeeDeleter(object sender, RoutedEventArgs e)
        {
            Employees selected = EmployeesList.SelectedItem as Employees;
            if (selected != null && manager.RemoveSelectedEmployee(selected))
            {
                ReloadList();
            }
        }

        private void EmployeeSearch(object sender, RoutedEventArgs e)
        {
            Window1 Search_window = new Window1(manager, EmployeesList);
            Search_window.Show();
        }

        private void ReloadList(object sender, RoutedEventArgs e)
        {
            ReloadList();
        }


        private void About_Button(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }
    }
}