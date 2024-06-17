using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_test
{

    public class Jobs
    {
        public string JobTitle { get; set; }
        public float JobCost { get; set; }
        public float HourlyRate {  get; set; }
        public JobProgress Progress { get; set; }
        public Contractors AssignedContractor { get; set; }

        /// <summary>
        /// Class for defining what a job is available
        /// </summary>
        /// <param name="jobTitle">Title of the job</param>
        /// <param name="jobCost">Total cost of the job being done/ budgeting</param>
        /// <param name="hourlyRate">Payment to the job</param>
        /// <param name="progress">A check for what the status of the job is</param>
        /// <param name="assignedContractor">Assigns a relationship between the contractors and the job. has a method to allow the job not having anyone assigned</param>
        public Jobs(string jobTitle, float jobCost, float hourlyRate, JobProgress progress, Contractors assignedContractor = null) 
        {
            JobTitle = jobTitle;
            JobCost = jobCost;
            HourlyRate = hourlyRate;
            Progress = progress;
            AssignedContractor = assignedContractor;

        }
        public override string ToString() 
        {
            string contractorInfo;
            if (AssignedContractor != null)
            {
                contractorInfo = $"{AssignedContractor.ID} | {AssignedContractor.FirstName} {AssignedContractor.LastName}";
            }
            else
            {
                contractorInfo = "None";
            }
            return $"Job: {JobTitle} \nCost: {JobCost} \nHourly Rate: {HourlyRate}/hr \nJob Progress: {Progress}" +
                $"\nContractor: {contractorInfo}";
        }

    }

    public enum JobProgress
    {
        Not_Assigned = 0,
        In_Progress = 1,
        Completed = 2
    }

}