using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AuthorDao
    {
        EWBOK_DbContext db = null;
        public AuthorDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<Author> ListAllAuthor()
        {
            return db.Authors.ToList();
        }
        public List<Author> ListAllAuthorActive()
        {
            return db.Authors.Where(x => x.Status == true).ToList();
        }
        public Author GetDetail(long id)
        {
            return db.Authors.Find(id);
        }
        public long Insert(Author AuthorEntity)
        {
            AuthorEntity.CreateDate = DateTime.Now;
            db.Authors.Add(AuthorEntity);
            db.SaveChanges();
            return AuthorEntity.ID;
        }
        public bool Update(Author AuthorEntity)
        {
            try
            {
                var author = db.Authors.Find(AuthorEntity.ID);
                if(!string.IsNullOrEmpty(AuthorEntity.Image))
                {
                    author.Image = AuthorEntity.Image;
                }
                if (!string.IsNullOrEmpty(AuthorEntity.Name))
                {
                    author.Name = AuthorEntity.Name;
                }
                if (!string.IsNullOrEmpty(AuthorEntity.NickName))
                {
                    author.NickName = AuthorEntity.NickName;
                }
                if (!string.IsNullOrEmpty(AuthorEntity.PlaceOfBirth))
                {
                    author.PlaceOfBirth = AuthorEntity.PlaceOfBirth;
                }
                if(!string.IsNullOrEmpty(AuthorEntity.Birth.ToString()))
                {
                    author.Birth = AuthorEntity.Birth;
                }
                if(!string.IsNullOrEmpty(AuthorEntity.DateOfDeath.ToString()))
                {
                    author.DateOfDeath = AuthorEntity.DateOfDeath;
                }
                author.ModifiedBy = AuthorEntity.ModifiedBy;
                author.ModifiedDate = DateTime.Now;
                author.MetaTitle = AuthorEntity.MetaTitle;
                author.Tag = AuthorEntity.Tag;
                author.Status = AuthorEntity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Remove(Author AuthorEntity)
        {
            try
            {
                var Author = db.Authors.Find(AuthorEntity.ID);
                db.Authors.Remove(Author);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var author = db.Authors.Find(id);
            author.Status = !author.Status;
            db.SaveChanges();
            return author.Status;
        }

        //kết hợp với ListProductByListAuthor
        public List<Author> ListAuthorBySearchKey(string searchkey)
        {
            return db.Authors.Where(x => (x.Name.ToUpper().Contains(searchkey.ToUpper()) || x.Tag.ToUpper().Contains(searchkey.ToUpper())) && x.Status == true).ToList();
        }
    }
}
