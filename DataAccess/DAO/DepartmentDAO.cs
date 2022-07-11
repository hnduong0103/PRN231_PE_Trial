using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    internal class DepartmentDAO
    {
        private static DepartmentDAO instance = null;
        private static readonly object instanceLock = new object();

        private DepartmentDAO()
        {
        }

        public static DepartmentDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new DepartmentDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Department> GetAll()
        {
            var listDepartments = new List<Department>();
            try
            {
                using (var context = new DepartmentEmployeePETrailContext())
                {
                    listDepartments = context.Departments.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listDepartments;
        }
    }
}
