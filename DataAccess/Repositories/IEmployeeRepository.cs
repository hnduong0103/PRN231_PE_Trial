using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IEmployeeRepository
    {
        Task<AccountDb> Authentication(string email, string password);
        IEnumerable<Employee> GetEmployeeList();
        Employee GetEmployeeById(int id);
        void Add(Employee Employee);
        void Delete(int id);
        void Update(int id, Employee Employee);
    }
}
