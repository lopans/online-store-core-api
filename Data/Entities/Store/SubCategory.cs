using Base;
using Base.DAL;
using System.Collections.Generic;

namespace Data.Entities.Store
{
    public class SubCategory: BaseEntity, IClientEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual FileData Image { get; set; }
        public int? ImageID { get; set; }
        public int? CategoryID { get; set; }
        public Category Category { get; set; }
        public List<Item> Items { get; set; }
    }
}
