using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperCrud.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter The Name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter The City!")]
        public string City { get; set; }
        [Required(ErrorMessage = "Enter The Address!")]
        public string Address { get; set; }
        static string strConnection = "Data Source=.;Initial Catalog=FB;Integrated Security=True";


        public static IEnumerable<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (IDbConnection con=new SqlConnection(strConnection))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                employees = con.Query<Employee>("GetEmployees").ToList();
                
            }
            return employees;
        }

        public static Employee GetEmployeeById(int? id)
        {
            Employee emp = new Employee();
            if (id == null)
            {
                return emp;
            }
            using (IDbConnection con=new SqlConnection(strConnection))
            {
                if (con.State == ConnectionState.Closed)
                
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                emp = con.Query<Employee>("GetEmployeeById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return emp;
        }

        public static int AddEmployee(Employee obj)
        {
            int i = 0;
            using (IDbConnection con = new SqlConnection(strConnection))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", obj.Name);
                parameters.Add("@City", obj.City);
                parameters.Add("@Address", obj.Address);
                i = con.Execute("AddNewEmp", parameters, commandType: CommandType.StoredProcedure);
            }
            return i;
        }

        public static int UpdateEmployee(Employee obj)
        {
            int i = 0;
            using (IDbConnection con = new SqlConnection(strConnection))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmpId", obj.Id);
                parameters.Add("@Name", obj.Name);
                parameters.Add("@City", obj.City);
                parameters.Add("@Address", obj.Address);
                i = con.Execute("UpdateEmployee", parameters, commandType: CommandType.StoredProcedure);
            }
            return i;
        }

        public static int DeleteEmployee(int id)
        {
            int i = 0;
            using(IDbConnection con=new SqlConnection(strConnection))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmpId", id);
                i = con.Execute("DeleteEmpById", parameters, commandType: CommandType.StoredProcedure);
            }
            return i;
        }
    }
}
