using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Store
{
    public class FilterModel
    {
        public string search { get; set; } = "";
        public int pageSize { get; set; } = 6;
        public int page { get; set; } = 1;
    }
}
