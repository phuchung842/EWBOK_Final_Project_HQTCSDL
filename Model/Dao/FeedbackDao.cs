using Model.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FeedbackDao
    {
        EWBOK_DbContext db = null;
        public FeedbackDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<Feedback> ListAllFeedback()
        {
            return db.Feedbacks.ToList();
        }
        public List<Feedback> ListFeedbackByViewStatusFalse()
        {
            return db.Feedbacks.Where(x => x.ViewStatus == false).OrderByDescending(x => x.CreateDate).ToList();
        }
        public Feedback GetDetail(int id)
        {
            return db.Feedbacks.Find(id);
        }
        public int Insert(Feedback FeedbackEntity)
        {
            FeedbackEntity.CreateDate = DateTime.Now;
            db.Feedbacks.Add(FeedbackEntity);
            db.SaveChanges();
            return FeedbackEntity.ID;
        }
        public bool Remove(Feedback FeedbackEntity)
        {
            try
            {
                var Feedback = db.Feedbacks.Find(FeedbackEntity.ID);
                db.Feedbacks.Remove(Feedback);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(Feedback FeedbackEntity)
        {
            try
            {
                var feedback = db.Feedbacks.Find(FeedbackEntity.ID);
                feedback.ViewStatus = FeedbackEntity.ViewStatus;
                feedback.Name = FeedbackEntity.Name;
                feedback.Phone = FeedbackEntity.Phone;
                feedback.Address = FeedbackEntity.Address;
                feedback.Content = FeedbackEntity.Content;
                feedback.Email = FeedbackEntity.Email;
                
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
