using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    internal class EmployeeDAO
    {
        private static EmployeeDAO instance = null;
        private static readonly object instanceLock = new object();

        private EmployeeDAO()
        {
        }

        public static EmployeeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new EmployeeDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<AccountDb> Authentication(string email, string password)
        {
            var context = new DepartmentEmployeePETrailContext();
            var list = context.AccountDbs.ToList();
            return list.Where(member =>
                   member.UserId.Equals(email, StringComparison.OrdinalIgnoreCase) && member.AccountPassword.Equals(password))
                .FirstOrDefault();
        }
        public List<Employee> GetAll()
        {
            /*var context = new DepartmentEmployeePETrailContext();
            return context.Employees.Include(o => o.Department).ToListAsync();*/
            var listEmployees = new List<Employee>();
            try
            {
                using (var context = new DepartmentEmployeePETrailContext())
                {
                    listEmployees = context.Employees.Include(d => d.Department).ToList();
                }
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listEmployees;
        }

        public Employee Get(int id)
        {
            Employee _employee = new Employee();
            try
            {
                using (var context = new DepartmentEmployeePETrailContext())
                {
                    _employee = context.Employees.Include(d => d.Department).SingleOrDefault(x => x.EmployeeId == id);
                }
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return _employee;
        }

        public void Add(Employee member)
        {
            try
            {
                using (var context = new DepartmentEmployeePETrailContext())
                {
                    context.Employees.Add(member);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var _employee = Get(id);
                if (_employee != null)
                {
                    using (var context = new DepartmentEmployeePETrailContext())
                    {
                        context.Employees.Remove(_employee);
                        context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("This product doesn't exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Employee member)
        {
            try
            {
                using (var context = new DepartmentEmployeePETrailContext())
                {
                    context.Entry<Employee>(member).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
