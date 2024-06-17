using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace SQL_PRJ
{
    public class Employees
    {



        public int EmployeeID {get;set;}
        public string FirstName { get;set;}
        public string LastName { get;set;}
        public DateTime DateOfBirth { get;set;}
        public string GenderIdentity { get;set;}
        public float GrossSalary { get;set;}
        public int SupervisorID { get;set;}
        public int BranchID { get;set;}
        public Employees()
        {


        }

        public Employees(int employeeID, string firstName, string lastName, DateTime dateOfBirth, string genderIdentity, float grossSalary, int supervisorID, int branchID)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            GenderIdentity = genderIdentity;
            GrossSalary = grossSalary;
            SupervisorID = supervisorID;
            BranchID = branchID;
        }

        public override string ToString()
        {
            return EmployeeID + " | " + FirstName + " " + LastName + " | DOB: " + 
                DateOfBirth.ToString("dd-MM-yyyy") + " | Gender: " + GenderIdentity
                + " \nSupervisorID: " + SupervisorID + " | BranchID: " + BranchID + " | Gross Salary: $" + GrossSalary + "\n" ;
        }
    }
}
