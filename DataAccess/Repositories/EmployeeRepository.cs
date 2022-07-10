using DataAccess.DAO;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task<AccountDb> Authentication(string email, string password)
        {
            return EmployeeDAO.Instance.Authentication(email, password);
        }

        public IEnumerable<Employee> GetEmployeeList()
        {
            try
            {
                var employeeList = EmployeeDAO.Instance.GetAll();
                return employeeList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                var employee = EmployeeDAO.Instance.Get(id);
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(Employee Employee)
        {
            try
            {
                EmployeeDAO.Instance.Add(Employee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                EmployeeDAO.Instance.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int id, Employee Employee)
        {
            try
            {
                Employee.EmployeeId = id;
                EmployeeDAO.Instance.Update(Employee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
