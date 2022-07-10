using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    internal class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        private UserDAO() { }
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        public AccountDb GetMemberLogin(String email, String password)
        {
            AccountDb user = null;
            try
            {
                var db = new DepartmentEmployeePETrailContext();
                user = db.AccountDbs.SingleOrDefault(user => user.UserId == email
                                                           && user.AccountPassword == password);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return user;
        }
    }
}
