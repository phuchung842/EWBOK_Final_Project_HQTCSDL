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
    public class RentDao
    {
        EWBOK_DbContext db = null;
        public RentDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<sp_ShowListProductToChecked> ListProductToChecked()
        {
            return db.Database.SqlQuery<sp_ShowListProductToChecked>("[dbo].[sp_ShowListProductToChecked]").ToList();
        }
        public List<Rent> ListAllRentByUserID(long id)
        {
            return db.Rents.Where(x => x.UserID == id && x.Status == true).ToList();
        }
        public List<sp_ShowListProductRent> ListDetailOfRentByUserID(long id)
        {
            SqlParameter param1 = new SqlParameter("@USERID", id);
            return db.Database.SqlQuery<sp_ShowListProductRent>("[dbo].[sp_ShowListProductRented] @USERID", param1).ToList();
        }
        public List<sp_ShowListProductRenting> ListRenting()
        {
            return db.Database.SqlQuery<sp_ShowListProductRenting>("[dbo].[sp_ShowListProductRenting]").ToList();
        }
        public string RentWithRegis(long userid,List<RentItem> ListRent)
        {
            string result = null;
            SqlParameter param1 = new SqlParameter("@USERID", userid);
            SqlParameter param2 = new SqlParameter("@RESULT", null)
            {
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID", typeof(long));
            dt.Columns.Add("DayRent", typeof(short));
            dt.Columns.Add("RentMoney", typeof(decimal));
            dt.Columns.Add("Deposit", typeof(decimal));
            for (int i = 0; i < ListRent.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["ProductID"] = ListRent[i].ProductID;
                dr["DayRent"] = ListRent[i].DayRent;
                dr["RentMoney"] = ListRent[i].RentMoney;
                dr["Deposit"] = ListRent[i].Deposit;
                dt.Rows.Add(dr);
            }
            SqlParameter listrent = new SqlParameter("@LIST_REGIS", SqlDbType.Structured)
            {
                Value = dt,
                TypeName= "dbo.LISTRENTWITHREGIS"
            };
            try
            {
                db.Database.ExecuteSqlCommand("[dbo].[sp_Rent_WithRegis] @USERID, @RESULT OUT, @LIST_REGIS", param1, param2, listrent);
            }
            catch(SqlException ex)
            {
                result = ex.Message.Substring(0, ex.Message.IndexOf(Environment.NewLine));
            }
            return result;
        }
        public string RentWithoutRegis(string name, string idnumber,string address,string email,string phone, List<RentItemWithoutRegis> ListRent)
        {
            string result = null;
            SqlParameter sqlname = new SqlParameter("@NAME", name);
            SqlParameter sqlidnumber = new SqlParameter("@IDNUMBER", idnumber);
            SqlParameter sqladdress = new SqlParameter("@ADDRESS", address);
            SqlParameter sqlemail = new SqlParameter("@EMAIL", email);
            SqlParameter sqlphone = new SqlParameter("@PHONE", phone);
            SqlParameter sqlresult = new SqlParameter("@RESULT", 0)
            {
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID", typeof(long));
            dt.Columns.Add("Quantity", typeof(short));
            dt.Columns.Add("DayRent", typeof(short));
            dt.Columns.Add("RentMoney", typeof(decimal));
            dt.Columns.Add("Deposit", typeof(decimal));
            for (int i = 0; i < ListRent.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["ProductID"] = ListRent[i].Item.ProductID;
                dr["Quantity"] = ListRent[i].Quantity;
                dr["DayRent"] = ListRent[i].Item.DayRent;
                dr["RentMoney"] = ListRent[i].Item.RentMoney;
                dr["Deposit"] = ListRent[i].Item.Deposit;
                dt.Rows.Add(dr);
            }
            SqlParameter listrent = new SqlParameter("@LIST_REGIS", SqlDbType.Structured)
            {
                Value = dt,
                TypeName = "dbo.LISTRENTWITHOUTREGIS"
            };
            try
            {
                db.Database.ExecuteSqlCommand("[dbo].[sp_Rent_WithoutRegis]  @LIST_REGIS, @NAME, @IDNUMBER, @ADDRESS, @EMAIL, @PHONE, @RESULT OUT", listrent, sqlname, sqlidnumber, sqladdress, sqlemail, sqlphone, sqlresult);
            }
            catch(SqlException ex)
            {
                result = ex.Message.Substring(0, ex.Message.IndexOf(Environment.NewLine));
            }
            return result;
        }

        public string ReturnRenting(List<sp_ShowListProductRenting> list)
        {
            string result = null;
            
            DataTable dt = new DataTable();
            dt.Columns.Add("RentID", typeof(long));
            dt.Columns.Add("RentProductID", typeof(long));
            for (int i = 0; i < list.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["RentID"] = list[i].RentID;
                dr["RentProductID"] = list[i].RentProductID;
                dt.Rows.Add(dr);
            }
            SqlParameter sqllist = new SqlParameter("@LIST", SqlDbType.Structured)
            {
                Value = dt,
                TypeName = "dbo.LISTRETURNRENT"
            };
            try
            {
                db.Database.ExecuteSqlCommand("[dbo].[sp_Return_Rent] @LIST", sqllist);
            }
            catch(SqlException ex)
            {
                result = ex.Message.Substring(0, ex.Message.IndexOf(Environment.NewLine));
            }
            return result;
        }
    }
}
