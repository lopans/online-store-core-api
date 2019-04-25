using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Store
{
    public class ListViewWrapModel
    {
        public ListViewWrapModel(IQueryable<dynamic> data, FilterModel filter)
        {
            Data = data;
            Filter = filter;
        }
        public FilterModel Filter { get; }
        public IQueryable<dynamic> Data { get; }
    }
}
