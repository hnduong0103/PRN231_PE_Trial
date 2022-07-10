using DataAccess.DAO;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository
    {
        public AccountDb Login(LoginModel model)
        {
            try
            {
                var user = UserDAO.Instance.GetMemberLogin(model.Email, model.Password);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
