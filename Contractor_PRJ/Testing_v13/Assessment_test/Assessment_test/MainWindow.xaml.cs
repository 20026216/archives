using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assessment_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataManager manager = new DataManager();
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Reloads the list on opening the window
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            ReloadList();

        }
        /// <summary>
        /// A method of clearing list boxes, then adding them back, otherwise it would stays the same even tho the lists have been manipulated
        /// </summary>
        private void ReloadList() // Added more independent reloads depending on the edge cases while searching
        {
            var (contractors, jobs) = manager.ReloadAllLists();

            AvailableContractorList.Items.Clear();
            contractors.ForEach(contractor => AvailableContractorList.Items.Add(contractor));

            Job_list.Items.Clear();
            jobs.ForEach(job => Job_list.Items.Add(job));

        }
        /// <summary>
        /// separate method of reloading only the contractors list
        /// </summary>
        private void ReloadAvailableContractorList()
        {
            var contractors = manager.ReloadAvailableContractorList();
            AvailableContractorList.Items.Clear();
            contractors.ForEach(contractor => AvailableContractorList.Items.Add(contractor));
        }
        /// <summary>
        /// separate method of reloasing only the job list
        /// </summary>
        private void ReloadJobList()
        {
            var jobs = manager.ReloadJobList();
            Job_list.Items.Clear();
            jobs.ForEach(job => Job_list.Items.Add(job));
        }
        /// <summary>
        /// A method of adding jobs in a specific job list (for filtering)
        /// </summary>
        /// <param name="jobs"></param>
        private void ReloadJobListWhileFiltered(List<Jobs> jobs)
        {
            Job_list.Items.Clear();
            foreach (Jobs job in jobs)
            {
                Job_list.Items.Add(job);
            }
        }
        /// <summary>
        /// A method of reloading the job list IF there is anything in the min cost and max cost text box (a wacky way of fixing the filtration bug)
        /// </summary>
        private void ReloadJobListWithCostRange() // ADDED TO INDIVIDUALISE THE RELOADING WITH JOB COST
        {
            if (!float.TryParse(textBoxMinCost.Text, out float minCost) || !float.TryParse(textBoxMaxCost.Text, out float maxCost))
            {
                ReloadJobList();
                return;
            }

            List<Jobs> filteredJobs = manager.SearchJobsInCostRange(minCost, maxCost);

            Job_list.Items.Clear();
            ReloadJobListWhileFiltered(filteredJobs);
        }


        private void AvailableContractorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contractors selected = AvailableContractorList.SelectedItem as Contractors;
            if (selected != null)
            {
                textBoxFirstName.Text = selected.FirstName;
                textBoxLastName.Text = selected.LastName;
            }
        }


        private void Adder_Click(object sender, RoutedEventArgs e)
        {
            string firstName = textBoxFirstName.Text;
            string lastName = textBoxLastName.Text;
            manager.CreateNewContractor(firstName, lastName);
            ReloadAvailableContractorList();
        }



        private void Remover_Click(object sender, RoutedEventArgs e)
        {
            Contractors selectedAvailable = AvailableContractorList.SelectedItem as Contractors;
            if (selectedAvailable != null && manager.RemoveSelectedContractor(selectedAvailable))
            {
                ReloadAvailableContractorList();
            }
        }


        private void Job_Adder_Click(object sender, RoutedEventArgs e)
        {
            Contractors selectedAvailable = AvailableContractorList.SelectedItem as Contractors;
            Jobs selectedJob = Job_list.SelectedItem as Jobs;
            if (!manager.Add_to_Job(selectedAvailable, selectedJob))
            {
                MessageBox.Show("Select from the Job list AND Contractors list");
            }
            ReloadAvailableContractorList();
            ReloadJobListWithCostRange();

        }
        private void Complete_Job_Click(object sender, RoutedEventArgs e) 
        {
            Jobs selectedJob = Job_list.SelectedItem as Jobs;
            if (manager.Complete_Job(selectedJob))
            {
                ReloadAvailableContractorList();
                ReloadJobListWithCostRange();
            }
        }
        private void Search_Job_Click(object sender, RoutedEventArgs e) 
        {

            float minCost;
            if (!float.TryParse(textBoxMinCost.Text, out minCost))
            {
                MessageBox.Show("Please enter a number");
                return;
            }
            float maxCost;
            if (!float.TryParse(textBoxMaxCost.Text, out maxCost))
            {
                MessageBox.Show("Please enter a number");
            }

            List<Jobs> filteredJobs = manager.SearchJobsInCostRange(minCost, maxCost);
            Job_list.Items.Clear();
            ReloadJobListWhileFiltered(filteredJobs);

        }
        private void Show_All_Jobs_Click(object sender, RoutedEventArgs e)
        {
            ReloadJobList();
        }

        private void Add_Job_Click(object sender, RoutedEventArgs e) // Decided not to reload job list with cost range here, could confuse the user by not seeing where the job went cause of the filtration
        {
            float Cost;
            if (!float.TryParse(textBoxJobCost.Text, out Cost))
            {
                MessageBox.Show("Please enter a number");
                return;
            }
            float HourlyRate;
            if (!float.TryParse(textBoxHourlyRate.Text, out HourlyRate))
            {
                MessageBox.Show("Please enter a number");
                return;
            }
            string JobName = textBoxJobName.Text;
            manager.CreateNewJob(JobName, Cost, HourlyRate);
            ReloadList();

        }

        private void Remove_Job_Click(object sender, RoutedEventArgs e)
        {
            Jobs selectedJob = Job_list.SelectedItem as Jobs;

            if (selectedJob != null && manager.RemoveSelectedJob(selectedJob))
            {
                ReloadJobListWithCostRange();
            }
        }

    }
}