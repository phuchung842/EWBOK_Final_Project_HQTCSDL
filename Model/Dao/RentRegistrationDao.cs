using Model.EF;
using Model.sp_Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class RentRegistrationDao
    {
        EWBOK_DbContext db = null;
        public RentRegistrationDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<sp_ShowAllListRegis> ListAllRegistration()
        {
            return db.Database.SqlQuery<sp_ShowAllListRegis>("[dbo].[sp_ShowListAllRegis]").ToList();
        }
        public List<sp_ShowListRegis> ListAllRegistration(long userid)
        {
            var param1 = new SqlParameter("@USERID", userid);
            return db.Database.SqlQuery<sp_ShowListRegis>("[dbo].[sp_ShowListRegis] @USERID",param1).ToList();
        }

        public int CancelRegis(int quantity, long productid,long userid)
        {
            var param1 = new SqlParameter("@USERID", userid);
            var param2 = new SqlParameter("@PRODUCTID", productid);
            var param3 = new SqlParameter("@QUANTITY", quantity);
            var param4 = new SqlParameter("@RESULT", null)
            {
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            db.Database.ExecuteSqlCommand("[dbo].[sp_CancelRegis] @USERID, @PRODUCTID, @QUANTITY, @RESULT OUT", param1, param2, param3, param4);
            var result = (int)param4.Value;
            return result;
        }
        public int Regis(long userid, short quantity, long productid)
        {
            //object[] sqlparams =
            //{
            //    new SqlParameter("@USERID",userid),
            //    new SqlParameter("@QUANTITY",quantity),
            //    new SqlParameter("@PRODUCTID",productid),
            //    new SqlParameter("@RESULT",null)
            //    {
            //        SqlDbType=SqlDbType.Int,
            //        Direction=ParameterDirection.Output
            //    }
            //};

            var param1 = new SqlParameter("@USERID", userid);
            var param2 = new SqlParameter("@QUANTITY", quantity);
            var param3 = new SqlParameter("@PRODUCTID", productid);
            var param4 = new SqlParameter("@RESULT", null)
            {
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
            };
            //var parameters = new List<SqlParameter>();
            //parameters.Add(param1);
            //parameters.Add(param2);
            //parameters.Add(param3);
            //parameters.Add(param4);

            db.Database.ExecuteSqlCommand("[dbo].[sp_RentRegistration] @USERID, @QUANTITY, @PRODUCTID, @RESULT OUT", param1, param2, param3, param4);
            var result = (int)param4.Value;
            return result;
        }
    }
}
