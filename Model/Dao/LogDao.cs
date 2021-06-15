using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class LogDao
    {
        EWBOK_DbContext db = null;
        public LogDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<Log> ListAllLog()
        {
            return db.Logs.OrderByDescending(x => x.CreateDate).ToList();
        }
        public bool SetLog(string message, string exception, long userid)
        {
            try
            {
                var log = new Log();
                log.CreateDate = DateTime.Now;
                log.UserID = userid;
                log.Message = message;
                log.Exception = exception;
                db.Logs.Add(log);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
