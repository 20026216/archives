using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace SQL_PRJ
{
    public class Database
    {
        private string dbname = "jcm_ictprg431";
        private string dbuser = "root";
        private string dbpassword = "";
        private int dbport = 3306;
        private string dbserver = "localhost";

        public List<Employees> read()
        {
            string connectionString = $"server={dbserver}; " +
                $"user={dbuser}; database={dbname}; port={dbport}; " +
                $"password={dbpassword}";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                List<Employees> result = new List<Employees>();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM employees", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(LoadEmployee(reader));
                }
                reader.Close();
                return result;
            }
        }
        private Employees LoadEmployee(MySqlDataReader reader)
        {
            int id = Convert.ToInt32(reader["employee_id"]);
            string fname = (string)reader["given_name"];
            string lname = (string)reader["family_name"];
            DateTime DOB = (DateTime)reader["date_of_birth"];
            string genderID = (string)reader["gender_identity"];
            int grossSalary = Convert.ToInt32(reader["gross_salary"]);
            int supervisorID; 
            if (reader["supervisor_id"] != DBNull.Value)
            {
                supervisorID = Convert.ToInt32(reader["supervisor_id"]);
            }
            else
            {
                supervisorID = 0;
            }
            int branchID = Convert.ToInt32(reader["branch_id"]);


            Employees employee = new Employees()
            {
                EmployeeID = id,
                FirstName = fname,
                LastName = lname,
                DateOfBirth = DOB,
                GenderIdentity = genderID,
                GrossSalary = grossSalary,
                SupervisorID = supervisorID,
                BranchID = branchID,
            };
            return employee;

        }

        public List<SearchEmployees> readEmployeesWithTotalSales()
        {
            string connectionString = $"server={dbserver}; " +
                $"user={dbuser}; database={dbname}; port={dbport}; " +
                $"password={dbpassword}";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                List<SearchEmployees> result = new List<SearchEmployees>();
                MySqlCommand cmd = new MySqlCommand("SELECT employees.employee_id, employees.given_name, employees.family_name, working_with.total_sales, clients.client_name FROM working_with INNER JOIN clients ON clients.client_id=working_with.client_id INNER JOIN employees ON employees.employee_id=working_with.employee_id", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(LoadEmployeeWithSales(reader));
                }
                reader.Close();
                return result;
            }
        }

        private SearchEmployees LoadEmployeeWithSales(MySqlDataReader reader)
        {
            int id = Convert.ToInt32(reader["employee_id"]);
            string fname = (string)reader["given_name"];
            string lname = (string)reader["family_name"];
            float tsales = Convert.ToInt32(reader["total_sales"]);
            string cname = (string)reader["client_name"];


            SearchEmployees employee = new SearchEmployees()
            {
                EmployeeID = id,
                FirstName = fname,
                LastName = lname,
                TotalSales = tsales,
                ClientName = cname
            };
            return employee;
        }

        public void AddEmployeeToDB(Employees employee)
        {
            string connectionString = $"server={dbserver}; " +
                $"user={dbuser}; database={dbname}; port={dbport}; " +
                $"password={dbpassword}";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO employees (employee_id, given_name, family_name, date_of_birth, gender_identity, gross_salary, supervisor_id, branch_id) " +
               "VALUES (@EmployeeID, @FirstName, @LastName, @DateOfBirth, @GenderIdentity, @GrossSalary, @SupervisorID, @BranchID)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@GenderIdentity", employee.GenderIdentity);
                cmd.Parameters.AddWithValue("@GrossSalary", employee.GrossSalary);
                cmd.Parameters.AddWithValue("@SupervisorID", employee.SupervisorID != 0 ? (object)employee.SupervisorID : DBNull.Value);
                cmd.Parameters.AddWithValue("@BranchID", employee.BranchID);

                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        public void RemoveEmployeeFromDB(int employeeID)
        {
            string connectionString = $"server={dbserver}; " +
                $"user={dbuser}; database={dbname}; port={dbport}; " +
                $"password={dbpassword}";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM employees WHERE employee_id = @EmployeeID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        public void UpdateEmployeeInDB(Employees employee)
        {
            string connectionString = $"server={dbserver}; " +
                                      $"user={dbuser}; database={dbname}; port={dbport}; " +
                                      $"password={dbpassword}";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE employees SET given_name = @FirstName, family_name = @LastName, date_of_birth = @DateOfBirth, " +
                               "gender_identity = @GenderIdentity, gross_salary = @GrossSalary, supervisor_id = @SupervisorID, branch_id = @BranchID " +
                               "WHERE employee_id = @EmployeeID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@GenderIdentity", employee.GenderIdentity);
                cmd.Parameters.AddWithValue("@GrossSalary", employee.GrossSalary);
                cmd.Parameters.AddWithValue("@SupervisorID", employee.SupervisorID != 0 ? (object)employee.SupervisorID : DBNull.Value);
                cmd.Parameters.AddWithValue("@BranchID", employee.BranchID);

                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        public List<Employees> SearchByNameInDB(string firstName, string lastName)
        {
            string connectionString = $"server={dbserver}; " +
                $"user={dbuser}; database={dbname}; port={dbport}; " +
                $"password={dbpassword}";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM employees WHERE given_name = @FirstName OR family_name = @LastName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.ExecuteNonQuery();
                List<Employees> result = new List<Employees>();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(LoadEmployee(reader));
                }
                reader.Close();
                return result;
            }
        }
        public List<Employees> SearchBySalaryInDB(float minSalary, float maxSalary)
        {
            string connectionString = $"server={dbserver}; " +
                $"user={dbuser}; database={dbname}; port={dbport}; " +
                $"password={dbpassword}";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM employees WHERE gross_salary >= @MinSalary AND gross_salary <= @MaxSalary";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MinSalary", minSalary);
                cmd.Parameters.AddWithValue("@MaxSalary", maxSalary);
                cmd.ExecuteNonQuery();
                List<Employees> result = new List<Employees>();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(LoadEmployee(reader));
                }
                reader.Close();
                return result;
            }

        }
        public List<Employees> SearchByBranchInDB(int branchID)
        {
            string connectionString = $"server={dbserver}; " +
                $"user={dbuser}; database={dbname}; port={dbport}; " +
                $"password={dbpassword}";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM employees WHERE branch_id = @BranchID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BranchID", branchID);
                cmd.ExecuteNonQuery();
                List<Employees> result = new List<Employees>();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(LoadEmployee(reader));
                }
                reader.Close();
                return result;
            }

        }

        public List<SearchEmployees> SearchSalesByID(int employeeID)
        {
            string connectionString = $"server={dbserver}; " +
                $"user={dbuser}; database={dbname}; port={dbport}; " +
                $"password={dbpassword}";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT employees.employee_id, employees.given_name, employees.family_name, working_with.total_sales, clients.client_name FROM working_with \r\nINNER JOIN clients ON clients.client_id=working_with.client_id \r\nINNER JOIN employees ON employees.employee_id=working_with.employee_id \r\nWHERE working_with.employee_id = @EmployeeID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.ExecuteNonQuery();
                List<SearchEmployees> result = new List<SearchEmployees>();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(LoadEmployeeWithSales(reader));
                }
                reader.Close();
                return result;
            }
        }
    }
}