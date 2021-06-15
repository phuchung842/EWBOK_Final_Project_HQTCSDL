using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class LoginDao
    {
        EWBOK_DbContext db = null;
        public LoginDao()
        {
            db = new EWBOK_DbContext();
        }
        public int Login(string UserName, string Password)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == UserName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == Password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }
    }
}
