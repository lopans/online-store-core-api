using Base.DAL;
using Common;
using Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;
using WebApi.Models.Store;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [EnableCors("any")]
    public class CoreControllerBase: ControllerBase
    {
        private readonly IApplicationContext _appContext;
        private IUnitOfWork uofw;
        private ISystemUnitOfWork suofw;
        public CoreControllerBase(DataContext context, IApplicationContext appContext)
        {
            _appContext = appContext;
        }
        public IUnitOfWork CreateUnitOfWork
        {
            get
            {
                var context = (DataContext)HttpContext.RequestServices.GetService(typeof(DataContext));
                if (uofw == null) uofw = new UnitOfWork(context);
                return uofw;
            }
        }

        public ISystemUnitOfWork CreateSystemUnitOfWork
        {
            get
            {
                var context = (DataContext)HttpContext.RequestServices.GetService(typeof(DataContext));

                if (suofw == null) suofw = new SystemUnitOfWork(context);
                return suofw;
            }
        }

        public async Task<ActionResult> WrapListViewResult(ListViewWrapModel model)
        {
            var canWrite = _appContext.IsAdmin() || _appContext.IsEditor();
            var total = model.Data.Count();
            var totalPages =  total % model.Filter.pageSize == 0 ?
                total / model.Filter.pageSize:
                (total / model.Filter.pageSize) + 1;
            var skip = (model.Filter.page - 1) * model.Filter.pageSize;

            var result = model.Data
                .Skip(skip)
                .Take(model.Filter.pageSize)
                .ToList();

            return Ok(new
            {
                Data = result,
                Create = canWrite,
                Update = canWrite,
                Delete = canWrite,
                Total = total,
                TotalPages = totalPages,
                Page = model.Filter.page
            });
        }
    }
}
