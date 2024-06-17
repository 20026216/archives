using Assessment_test;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Contractor_prj_testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class DataManager_testing
        {
            private DataManager _dataManager;
            [TestInitialize]
            
            public void Setup()
            {
                _dataManager = new DataManager();
            }
            /// <summary>
            /// Tests the reloads, see if it adds the joba and contractors correctly (counts the lists)
            /// </summary>
            [TestMethod]
            public void TestReloads() // more reloads added
            {
                var (contractors, jobs) = _dataManager.ReloadAllLists();

                Assert.IsNotNull(contractors);
                Assert.AreEqual(3, contractors.Count);
                Assert.IsNotNull(jobs);
                Assert.AreEqual(4, jobs.Count);

                var contractor = _dataManager.ReloadAvailableContractorList();

                Assert.IsNotNull(contractor);
                Assert.AreEqual(3, contractor.Count);

                var job = _dataManager.ReloadJobList();
                Assert.IsNotNull(job);
                Assert.AreEqual(4, job.Count);
            }
            /// <summary>
            /// Checks if the ID when adding a new contractor gives them a unique ID 
            /// (incrementing by one), also checks assigned contractors in job list
            /// </summary>
            [TestMethod]
            public void MaxIDReturninCorrectIDFrom2Lists()
            {
                var newContractors = new List<Contractors>
            {
                new Contractors (5, "John", "Cena"),
                new Contractors (6, "Walter", "White"),
                new Contractors (8, "Green", "Gobbler")
            };

                var newJobs = new List<Jobs>
            {
                new Jobs("Cooking", 5000, 20, JobProgress.In_Progress, new Contractors(7, "Howard", "Hamlin"))
            };
                foreach(var contractor  in newContractors)
                {
                    _dataManager.AddContractors (contractor);
                }
                foreach (var job in newJobs)
                {
                    _dataManager.AddJob (job);
                }

                int maxID = _dataManager.GetMaxID();
                Assert.AreEqual(8, maxID);

                string FirstName = "Greg", LastName = "Gregory";

                var newContractor = _dataManager.CreateNewContractor(FirstName, LastName);
                int expected_ID = 9;
                int actual_ID = newContractor.ID;

                Assert.IsNotNull(newContractor);
                Assert.AreEqual(FirstName, newContractor.FirstName);
                Assert.AreEqual(LastName, newContractor.LastName);
                Assert.AreEqual(expected_ID, actual_ID);

            }
            /// <summary>
            /// Checks if a new contractor is created matching their names
            /// </summary>
            [TestMethod]
            public void TestingContractorCreation()
            {
                string FirstName = "Greg", LastName = "Gregory";

                var newContractor = _dataManager.CreateNewContractor(FirstName, LastName);

                Assert.IsNotNull(newContractor);
                Assert.AreEqual(FirstName, newContractor.FirstName);
                Assert.AreEqual(LastName, newContractor.LastName);

            }
            /// <summary>
            /// Checks if a contractor doesn't exist after removing
            /// </summary>
            [TestMethod]
            public void TestingContractorRemoval()
            {
                var contractor = new Contractors(1, "Hugh", "Mungus");
                _dataManager.AddContractors(contractor);

                var result = _dataManager.RemoveSelectedContractor(contractor);

                Assert.IsTrue(result);
                Assert.IsFalse(_dataManager.GetContractors().Contains(contractor));
            }

            /// <summary>
            /// Checks if adding a contractor to a job changes progress to in progress,
            /// removes contractor from contractor list, and adds contractor into the assigned contractor in the job list
            /// </summary>
            [TestMethod]
            public void TestingContractoraddingtojob()
            {
                var contractor = new Contractors(1, "Hugh", "Mungus");
                var job = new Jobs("Tricycle", 10000, 30, JobProgress.Not_Assigned);

                var result = _dataManager.Add_to_Job(contractor, job);

                Assert.IsTrue(result);
                Assert.AreEqual(contractor, job.AssignedContractor);
                Assert.AreEqual(JobProgress.In_Progress, job.Progress);
                Assert.IsFalse(_dataManager.GetContractors().Contains(contractor));
            }
            /// <summary>
            /// Checks if completing a jobe returns contractor back to the contractor list, 
            /// changes progress and removes assigned contractor.
            /// </summary>
            [TestMethod]
            public void TestingJobCompletion()
            {
                var contractor = new Contractors(1, "Hugh", "Mungus");
                var job = new Jobs("Tricycle", 10000, 30, JobProgress.In_Progress) { AssignedContractor = contractor };

                var result = _dataManager.Complete_Job(job);

                Assert.IsTrue(result);
                Assert.AreEqual(JobProgress.Completed, job.Progress);
                Assert.IsNull(job.AssignedContractor);
                Assert.IsTrue(_dataManager.GetContractors().Contains(contractor));
            }
            /// <summary>
            /// Checks if jobs in the new list are within the cost range
            /// </summary>
            [TestMethod]
            public void TestingSearchFiltration()
            {
                float minCost = 5000; float maxCost = 9999;
                _dataManager.AddJob(new Jobs("Tricycle", 10000, 30, JobProgress.Not_Assigned));
                _dataManager.AddJob(new Jobs("Cooking", 5000, 20, JobProgress.In_Progress, new Contractors(7, "Howard", "Hamlin")));
                _dataManager.AddJob(new Jobs("Data Broker", 9000, 40, JobProgress.Not_Assigned));

                List<Jobs> filteredjobs = _dataManager.SearchJobsInCostRange(minCost, maxCost);

                Assert.AreEqual(1, filteredjobs.Count);
                Assert.IsTrue(filteredjobs.Exists(job => job.JobTitle == "Data Broker"));
            }
            /// <summary>
            /// Checks job creation, see if it is created with the correct job progress
            /// </summary>
            [TestMethod]
            public void TestingJobCreation()
            {
                string jobName = "Mewing";
                float Cost = 17000;
                float HourlyRate = 10;

                var newJob = _dataManager.CreateNewJob(jobName, Cost, HourlyRate);

                Assert.IsNotNull(newJob);
                Assert.AreEqual(Cost, newJob.JobCost);
                Assert.AreEqual(jobName, newJob.JobTitle);
                Assert.AreEqual(JobProgress.Not_Assigned, newJob.Progress);
                Assert.AreEqual(HourlyRate, newJob.HourlyRate);
                Assert.IsNull(newJob.AssignedContractor);
            }
            /// <summary>
            /// Checks job removal, see if it exists
            /// </summary>
            [TestMethod]
            public void TestingJobRemoval()
            {
                var contractor = new Contractors(1, "Hugh", "Mungus");
                var job = new Jobs("Tricycle", 10000, 30, JobProgress.Not_Assigned) { AssignedContractor = contractor };
                _dataManager.AddJob(job);

                var result = _dataManager.RemoveSelectedJob(job);

                Assert.IsTrue(result);
                Assert.IsFalse(_dataManager.GetJobs().Contains(job));
                Assert.IsTrue(_dataManager.GetContractors().Contains(contractor));
            }

        }
    }
}