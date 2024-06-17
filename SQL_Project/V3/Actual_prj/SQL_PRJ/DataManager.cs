using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;

namespace SQL_PRJ
{
    public class DataManager
    {
        Database database = new Database();
        private List<Employees> moreEmployees = new List<Employees>();
        private List<SearchEmployees> employeesWSales= new List<SearchEmployees>();
        public DataManager()
        {
            moreEmployees = database.read();
            employeesWSales = database.readEmployeesWithTotalSales();
        }

        public List<SearchEmployees> GetEmployeesWSales()
        {
            return employeesWSales;
        }

        public List<SearchEmployees> ReloadEmployeesWSales()
        {
            List<SearchEmployees> searchEmployees = GetEmployeesWSales();
            return searchEmployees;
        }

        public List<Employees> GetEmployees()
        {
            return moreEmployees;
        }

        public List<Employees> ReloadEmployeeList()
        {
            List<Employees> employees = GetEmployees();
            return employees;
        }
        public int GetMaxID()
        {
            int maxID = 0;
            foreach (Employees employee in moreEmployees)
            {
                if (employee.EmployeeID > maxID)
                {
                    maxID = employee.EmployeeID;
                }
            }

            return maxID;
        }

        public void AddEmployees(Employees employee)
        {
            moreEmployees.Add(employee);
        }
        public void RemoveEmployees(Employees employee)
        {
            moreEmployees.Remove(employee);
        }

        public Employees CreateNewEmployee(string GivenName, string FamilyName, DateTime DOB, string GenderIdentity,
            int SupervisorID, int BranchID, int Salary)
        {
            int employeeID = GetMaxID() + 1;
            Employees newEmployees = new Employees()
            {
                EmployeeID = employeeID,
                FirstName = GivenName,
                LastName = FamilyName,
                DateOfBirth = DOB,
                GenderIdentity = GenderIdentity,
                SupervisorID = SupervisorID,
                BranchID = BranchID,
                GrossSalary = Salary
            };
            AddEmployees(newEmployees);
            database.AddEmployeeToDB(newEmployees);
            return newEmployees;
        }

        public bool RemoveSelectedEmployee(Employees selectedEmployee)
        {
            if (selectedEmployee != null)
            {
                RemoveEmployees(selectedEmployee);
                database.RemoveEmployeeFromDB(selectedEmployee.EmployeeID);
                return true;
            }
            return false;
        }


        private Employees FindEmployeeByID(int employeeID)
        {
            foreach (var emp in moreEmployees)
            {
                if (emp.EmployeeID == employeeID)
                {
                    return emp;
                }
            }
            return null;
        }
        public void UpdateEmployeeInList(Employees updatedEmployee)
        {
            Employees employee = FindEmployeeByID(updatedEmployee.EmployeeID);
            if (employee != null)
            {
                employee.FirstName = updatedEmployee.FirstName;
                employee.LastName = updatedEmployee.LastName;
                employee.DateOfBirth = updatedEmployee.DateOfBirth;
                employee.GenderIdentity = updatedEmployee.GenderIdentity;
                employee.GrossSalary = updatedEmployee.GrossSalary;
                employee.SupervisorID = updatedEmployee.SupervisorID;
                employee.BranchID = updatedEmployee.BranchID;
                database.UpdateEmployeeInDB(updatedEmployee);
            }
        }
        // ALL OF THIS IS NOW REDUNDANT, now all searching is done by database instead
        /*public List<Employees> SearchEmployeesByName(string firstName, String lastName)
        {
            List<Employees> employees = GetEmployees();
            List<Employees> filteredEmployees = new List<Employees>();

            foreach (Employees employee in employees)
            {
                if (employee.FirstName.Equals(firstName) && employee.LastName.Equals(lastName))
                {
                    filteredEmployees.Add(employee);
                }
                else if (employee.FirstName.Equals(firstName) || employee.LastName.Equals(lastName))
                {
                    filteredEmployees.Add((Employees) employee);
                }
            }

            return filteredEmployees;
        }

        public List<Employees> SearchEmployeesBySalary(float minSalary, float maxSalary)
        {
            List<Employees> employees = GetEmployees();
            List<Employees> filteredEmployees = new List<Employees>();

            foreach (Employees employee in employees)
            {
                if (employee.GrossSalary > minSalary && employee.GrossSalary <= maxSalary)
                {
                    filteredEmployees.Add(employee);
                }
            }

            return filteredEmployees;
        }


        public List<Employees> SearchEmployeesByBranch(int branchID)
        {
            List<Employees> employees = GetEmployees();
            List<Employees> filteredEmployees = new List<Employees>();

            foreach (Employees employee in employees)
            {
                if (employee.BranchID == branchID)
                {
                    filteredEmployees.Add(employee);
                }
            }

            return filteredEmployees;
        }*/



    }

}
