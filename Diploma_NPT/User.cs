using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_NPT
{
    public class User
    {
        private int iD;
        private string login;
        private string password;

        public int ID { get => iD; }
        public string Login { get => login; }
        public string Password { get => password; }

        public User(int iD, string login, string password)
        {
            this.iD = iD;
            this.login = login;
            this.password = password;
        }

        public User(string info)
        {
            if (info != null)
            {
                string[] values = info.Split('|');

                if (values.Length >= 2)
                {
                    try { iD = Convert.ToInt32(values[0]); } catch { iD = -1; }
                    try { login = values[1]; } catch (Exception ex) { login = ""; }
                    try { password = values[2]; } catch (Exception ex) { password = ""; }
                }
            }
        }
    }
}
