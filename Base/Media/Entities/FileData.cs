using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.DAL
{
    public class FileData : BaseEntity, IClientEntity
    {
        public Guid FileID { get; set; }
        public string FileName { get; set; }
        [MaxLength(20)]
        public string Extension { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
