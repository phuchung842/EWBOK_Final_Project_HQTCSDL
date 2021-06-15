using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWBOK_Final_Project.Common
{
    public class ProductComparer: IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            //First check if both object reference are equal then return true
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            //If either one of the object refernce is null, return false
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }
            //Comparing all the properties one by one
            return x.ID == y.ID && x.Name == y.Name && x.Code == y.Code && x.MetaTitle == y.MetaTitle && x.Decription == y.Decription && x.Image == y.Image && x.MoreImage == y.MoreImage && x.Price == y.Price && x.PromotionPrice == y.PromotionPrice && x.Translator == y.Translator && x.Weight == y.Weight && x.Pages == y.Pages && x.Size == y.Size && x.Form == y.Form && x.IncludeVAT == y.IncludeVAT && x.Quantity == y.Quantity && x.PublishYear == y.PublishYear && x.PublisherID == y.PublisherID && x.AuthorID == y.AuthorID && x.CategoryID == y.CategoryID && x.Detail == y.Detail && x.Warranty == y.Warranty && x.CreateDate == y.CreateDate && x.CreatedBy == y.CreatedBy && x.ModifiedDate == y.ModifiedDate && x.ModifiedBy == y.ModifiedBy && x.MetaKeywords == y.MetaKeywords && x.MetaDecription == y.MetaDecription && x.Status == y.Status && x.TopHot == y.TopHot && x.ViewCount == y.ViewCount && x.ProductStatus == y.ProductStatus && x.Star == y.Star;
        }

        public int GetHashCode(Product obj)
        {
            //If obj is null then return 0
            if (obj == null)
            {
                return 0;
            }
            //Get the ID hash code value
            int IDHashCode = obj.ID.GetHashCode();
            //Get the string HashCode Value
            //Check for null refernece exception
            int NameHashCode = obj.Name == null ? 0 : obj.Name.GetHashCode();
            return IDHashCode ^ NameHashCode;
        }
    }
}