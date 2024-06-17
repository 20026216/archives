using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_test
{
    public class DataManager
    {
        private List<Contractors> moreContractors = new List<Contractors>();

        public DataManager()
        {
            moreContractors.Add(new Contractors(1, "Bruh", "Moment", JobStatus.Job_Assigned));
        }
    }

    public List<Contractors> ShowContractors()
    {
        return moreContractors;
    }

    public void AddContractors(Contractors contractor)
    {
        contractors.Add(contractor)
    }

}