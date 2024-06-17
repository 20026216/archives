using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_PRJ
{
    public class SearchEmployees
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float TotalSales { get; set; }
        public string ClientName {  get; set; }


        public SearchEmployees()
        {

        }

        public SearchEmployees(int employeeID, string firstName, string lastName, float totalSales, string clientName)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            TotalSales = totalSales;
            ClientName = clientName;
        }

        public override string ToString()
        {
            return EmployeeID + " | " + ClientName + " \n " + FirstName + " " + LastName + " Total Sales: " + TotalSales + "\n";
        }
    }

}
