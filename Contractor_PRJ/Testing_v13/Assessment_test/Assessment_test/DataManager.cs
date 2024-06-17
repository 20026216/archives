using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Assessment_test
{
    public class DataManager
    {
        private List<Contractors> moreContractors = new List<Contractors>();
        private List<Jobs> moreJobs = new List<Jobs>();
        /// <summary>
        /// Adds dummy data to the lists
        /// </summary>
        /// <returns>Puts 3 contractors and 4 jobs onto their respective listboxes</returns>
        public DataManager()
        {
            moreContractors.Add(new Contractors(1, "Bruh", "Moment"));
            moreContractors.Add(new Contractors(2, "Collin", "Jobs"));
            moreContractors.Add(new Contractors(3, "Simple", "Simon"));
            moreJobs.Add(new Jobs("Tunnel Boring", 200000, 45, JobProgress.Not_Assigned));
            moreJobs.Add(new Jobs("Cleaning", 5000, 20, JobProgress.Not_Assigned));
            moreJobs.Add(new Jobs("Streaming", 3000, 30, JobProgress.Not_Assigned));
            moreJobs.Add(new Jobs("Going Ham", 20000, 43, JobProgress.Not_Assigned));
        }
        /// <summary>
        /// Method to put all contractors into the contractor list
        /// </summary>
        public List<Contractors> GetContractors()
        {
            return moreContractors;
        }
        /// <summary>
        /// Method to add contractor into the contractor list
        /// </summary>
        public void AddContractors(Contractors contractor)
        {
            moreContractors.Add(contractor);
        }
        /// <summary>
        /// Method to remove contractor from the contractor list
        /// </summary>
        public void RemoveContractors(Contractors contractor)
        {
            moreContractors.Remove(contractor);
        }
        /// <summary>
        /// Method to put all jobs into the job list
        /// </summary>
        public List<Jobs> GetJobs()
        {
            return moreJobs;
        }
        /// <summary>
        /// Adds job into job list
        /// </summary>
        public void AddJob(Jobs jobs)
        {
            moreJobs.Add(jobs);
        }
        /// <summary>
        /// Removes job from the job list
        /// </summary>
        public void RemoveJob(Jobs jobs)
        {
            moreJobs.Remove(jobs);
        }
        /// <summary>
        /// A separate reloading method to grab only contractors
        /// </summary>
        /// <returns>a list of contractors</returns>
        public List<Contractors> ReloadAvailableContractorList()
        {
            List<Contractors> contractors = GetContractors();
            return contractors;
        }
        /// <summary>
        /// Same thing but for jobs
        /// </summary>
        /// <returns>a list of jobs</returns>
        public List<Jobs> ReloadJobList()
        {
            List<Jobs> jobs = GetJobs();
            return jobs;
        }
        /// <summary>
        /// now returns both jobs (reloads has been done this way so it could be unit tested)
        /// </summary>
        /// <returns>a list of contractors and jobs</returns>
        public (List<Contractors>, List<Jobs>) ReloadAllLists()
        {
            List<Contractors> contractors = ReloadAvailableContractorList();
            List<Jobs> jobs = ReloadJobList();
            return (contractors, jobs);
        }

        /// <summary>
        /// This was added so that the ID can be unique everytime there's a new contractor added, in case they have similar names
        /// </summary>
        /// <returns>The max ID from both lists (checks assigned contractors ID as well)</returns>
        public int GetMaxID()
        {
            int maxID = 0;
            foreach (Contractors contractor in moreContractors)
            {
                if (contractor.ID > maxID)
                {
                    maxID = contractor.ID;
                }
            }
            foreach (Jobs assignedcontractor in moreJobs)
            {
                if (assignedcontractor.AssignedContractor != null && assignedcontractor.AssignedContractor.ID > maxID)
                {
                    maxID = assignedcontractor.AssignedContractor.ID;
                }

            }
            return maxID;
        }
        /// <summary>
        /// Method for adding a contractor while calculating a brand new ID
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>returns a contractor object</returns>
        public Contractors CreateNewContractor(string firstName, string lastName)
        {
            int contractor_ID = GetMaxID() + 1; 
            Contractors newcontractor = new Contractors(contractor_ID, firstName, lastName);
            AddContractors(newcontractor);
            return newcontractor;
        }
        /// <summary>
        /// A method of removing a contractor using a boolean
        /// </summary>
        /// <param name="selectedContractor"></param>
        /// <returns>needs a contractor that is selected from a list to return true, then removes that contractor.</returns>
        public bool RemoveSelectedContractor(Contractors selectedContractor) 
        {
            if (selectedContractor != null)
            {
                RemoveContractors(selectedContractor);
                return true;
            }
            return false;
        }
        /// <summary>
        /// A method of adding a contractor to a job, while changing its progress
        /// </summary>
        /// <param name="selectedContractor"></param>
        /// <param name="selectedJob"></param>
        /// <returns>returns true if there is a selected job and contractor, then removes the selected contractor from the contractor list (preventing duplication)</returns>
        public bool Add_to_Job(Contractors selectedContractor, Jobs selectedJob)
        {
            if (selectedJob == null || selectedContractor == null)
            {
                return false;
            }
            selectedJob.AssignedContractor = selectedContractor;
            selectedJob.Progress = JobProgress.In_Progress;
            RemoveContractors(selectedContractor);
            return true;
        }
        /// <summary>
        /// A method to complete a job
        /// </summary>
        /// <param name="selectedJob"></param>
        /// <returns>returns true if there is a selected job, then removes that assigned contractor and returns it into the contractor list</returns>
        public bool Complete_Job(Jobs selectedJob)
        {
            if (selectedJob == null)
            {
                MessageBox.Show("Select a job from the job list to complete");
                return false;
            }
            else if (selectedJob.Progress == JobProgress.Completed)
            {
                MessageBox.Show("This Job is Completed");
                return false;
            }
            else
            {
                selectedJob.Progress = JobProgress.Completed;
                AddContractors(selectedJob.AssignedContractor);
                selectedJob.AssignedContractor = null;
                return true;
            }
        }
        /// <summary>
        /// Filters the job list based on the cost range
        /// </summary>
        /// <param name="minCost"></param>
        /// <param name="maxCost"></param>
        /// <returns>returns a list of jobs within the min cost and the max cost</returns>
        public List<Jobs> SearchJobsInCostRange(float minCost, float maxCost)
        {
            List<Jobs> jobs = GetJobs();
            List<Jobs> filteredJobs = new List<Jobs>();

            foreach (Jobs job in jobs)
            {
                if (job.JobCost > minCost && job.JobCost <= maxCost)
                {
                    filteredJobs.Add(job);
                }
            }

            return filteredJobs;
        }
        /// <summary>
        /// Creates a new job into the job list
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="Cost"></param>
        /// <param name="HourlyRate"></param>
        /// <returns>returns a new job using the jobname, cost and hourly rate</returns>
        public Jobs CreateNewJob(string jobName, float Cost, float HourlyRate)
        {
            Jobs newJob = new Jobs(jobName, Cost, HourlyRate, JobProgress.Not_Assigned);
            newJob.AssignedContractor = null;
            AddJob(newJob);
            return newJob;
        }
        /// <summary>
        /// A method to remove a job from the job list
        /// </summary>
        /// <param name="selectedJob"></param>
        /// <returns>returns true if there is a job that is selected to be removed, also checks assigned contractor in the job to be returned to the contractor list</returns>
        public bool RemoveSelectedJob(Jobs selectedJob)
        {
            if (selectedJob == null)
            {
                return false;
            }

            if (selectedJob.AssignedContractor != null)
            {
                AddContractors(selectedJob.AssignedContractor);
            }

            RemoveJob(selectedJob);
            return true;
        }
        

    }
    


}