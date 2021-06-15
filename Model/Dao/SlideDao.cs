using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SlideDao
    {
        EWBOK_DbContext db = null;
        public SlideDao()
        {
            db = new EWBOK_DbContext();
        }
        public List<Slide> ListAllSilde()
        {
            return db.Slides.OrderByDescending(x => x.CreateDate).ToList();
        }
        public List<Slide> ListAllActiveSlide()
        {
            return db.Slides.Where(x => x.Status).OrderByDescending(x => x.CreateDate).ToList();
        }
        public long Insert(Slide SlideEntity)
        {
            SlideEntity.CreateDate = DateTime.Now;
            db.Slides.Add(SlideEntity);
            db.SaveChanges();
            return SlideEntity.ID;
        }
        public bool Update(Slide SlideEntity)
        {
            try
            {
                var slide = db.Slides.Find(SlideEntity.ID);
                if (!string.IsNullOrEmpty(SlideEntity.Image))
                {
                    slide.Image = SlideEntity.Image;
                }
                slide.Decription = SlideEntity.Decription;
                slide.DisplayOrder = SlideEntity.DisplayOrder;
                slide.Link = SlideEntity.Link;
                slide.ModifiedDate = DateTime.Now;
                slide.ModifiedBy = SlideEntity.ModifiedBy;
                slide.Status = SlideEntity.Status;
                slide.Title = SlideEntity.Title;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Remove(Slide SlideEntity)
        {
            try
            {
                var slide = db.Slides.Find(SlideEntity.ID);
                db.Slides.Remove(slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Slide GetDetail(long id)
        { 
            return db.Slides.Find(id);
        }
        public bool ChangeStatus(long id)
        {
            var slide = db.Slides.Find(id);
            slide.Status = !slide.Status;
            db.SaveChanges();
            return slide.Status;
        }
    }
}
