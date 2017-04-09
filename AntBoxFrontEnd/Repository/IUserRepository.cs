using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntBoxFrontEnd.Models;

namespace AntBoxFrontEnd.Repository
{
    public interface IUserRepository :IDisposable
    {
        Users GetUser(Users pUser);
        Users GetUserByToken(string pToken);
        bool CreateUser(Users pUser);
        bool UpdateUser(Users pUser);
        bool ValidateUser(string pToken);
        void Save();
    }
}
