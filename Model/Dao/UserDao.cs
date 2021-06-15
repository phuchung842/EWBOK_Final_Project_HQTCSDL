using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        EWBOK_DbContext db = null;
        public UserDao()
        {
            db = new EWBOK_DbContext();
        }
        public long Insert(User UserEntity)
        {
            UserEntity.CreateDate = DateTime.Now;
            db.Users.Add(UserEntity);
            db.SaveChanges();
            return UserEntity.ID;
        }
        public bool Update(User UserEntity)
        {
            try
            {
                var user = db.Users.Find(UserEntity.ID);
                if (!string.IsNullOrEmpty(UserEntity.Password))
                {
                    user.Password = UserEntity.Password;
                }
                if (!string.IsNullOrEmpty(UserEntity.ImagePath))
                {
                    user.ImagePath = UserEntity.ImagePath;
                }
                if(!string.IsNullOrEmpty(UserEntity.Name))
                {
                    user.Name = UserEntity.Name;
                }
                if (!string.IsNullOrEmpty(UserEntity.Address))
                {
                    user.Address = UserEntity.Address;
                }
                if (!string.IsNullOrEmpty(UserEntity.Phone))
                {
                    user.Phone = UserEntity.Phone;
                }
                if (!string.IsNullOrEmpty(UserEntity.Email))
                {
                    user.Email = UserEntity.Email;
                }
                user.ConfirmStatus = UserEntity.ConfirmStatus;
                user.ModifiedBy = UserEntity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                user.GroupID = UserEntity.GroupID;
                user.Status = UserEntity.Status;
                user.ColorWebsite = UserEntity.ColorWebsite;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Remove(User UserEntity)
        {
            try
            {
                var User = db.Users.Find(UserEntity.ID);
                db.Users.Remove(User);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<User> ListAll()
        {
            return db.Users.ToList();
        }
        public User Detail(long id)
        {
            return db.Users.Find(id);
        }

        public User GetByUsername(string username)
        {
            return db.Users.SingleOrDefault(x => x.UserName == username);
        }
        public bool CheckByUsername(string username)
        {
            return db.Users.Count(x => x.UserName == username) > 0;
        }
        public bool CheckByEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
        public bool CheckByEmailInEdit(string email)
        {
            return db.Users.Count(x => x.Email == email) > 1;
        }
        public bool CheckByCodeConfirm(string code)
        {
            return db.Users.Count(x => x.CodeConfirm == code) > 0;
        }
        public bool CheckByPassword(string password)
        {
            return db.Users.Count(x => x.Password == password) > 0;
        }
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
    }
}