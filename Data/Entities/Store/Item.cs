using Base;
using Base.DAL;
using System.Collections.Generic;

namespace Data.Entities.Store
{
    public class Item: BaseEntity, IClientEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int SubCategoryID { get; set; }
        public SubCategory SubCategory { get; set; }
        public int? ImageID { get; set; }
        public FileData Image { get; set; }
        public string Link { get; set; }
    }
}
