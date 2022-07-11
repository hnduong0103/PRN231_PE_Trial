using DataAccess.DAO;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public IEnumerable<Department> GetDepartmentList()
        {
            try
            {
                var departmentList = DepartmentDAO.Instance.GetAll();
                return departmentList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
