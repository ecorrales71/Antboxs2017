using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Infrastructure;

namespace AntBoxFrontEnd.Repository
{
    public class UserRepository : IUserRepository
    {
        private AntBoxContext _context;

        public UserRepository(AntBoxContext context)
        {
            _context = context;
        }

        public bool CreateUser(Users pUser)
        {
            try
            {
                pUser.token = Code.HashHelper.GenerateToken();
                pUser.status = false;
                pUser.step = 1;
                _context.Users.Add(pUser);
                Save();
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
                return false;
            }
            return true;
        }

        public Users GetUser(Users pUser)
        {
            try
            {
                return _context.Users.Where(x => x.email == pUser.email && x.customerid == pUser.customerid).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
                return null;
            }
        }

        public Users GetUserByToken(string pToken)
        {
            try
            {
                if (!String.IsNullOrEmpty(pToken))
                    return _context.Users.Where(x => x.token == pToken).FirstOrDefault();
                else
                    return null;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
                return null;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool UpdateUser(Users pUser)
        {
            bool retorno = false;
            try
            {
                var aux = _context.Users.Where(x => x.customerid == pUser.customerid).FirstOrDefault();
                if (aux != null)
                {
                    aux.email = pUser.email;
                    Save();
                }
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
            }
            return retorno;
        }

        public bool ValidateUser(string pToken)
        {
            bool retorno = false;
            try
            {
                if (!String.IsNullOrEmpty(pToken))
                {
                    var aux = _context.Users.Where(x => x.token == pToken).FirstOrDefault();
                    if (aux != null)
                    {
                        if (Convert.ToBoolean(aux.status))
                        {
                            retorno = true;
                        }
                        else
                        {
                            if (TokenExpirate(aux.token))
                            {
                                aux.status = true;
                                Save();
                                retorno = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
            }
            return retorno;
        }

        public bool TokenExpirate(string pToken)
        {
            bool retorno = false;
            if (!String.IsNullOrEmpty(pToken))
            {
                pToken = Code.HashHelper.Base64Decode(pToken);
                var split = pToken.Split('.');
                int Value = DateTime.Compare(DateTime.UtcNow, Convert.ToDateTime(Code.HashHelper.Base64Decode(split[2]).ToString()));
                if (Value < 1)
                    retorno = true;
            }
            return retorno;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}