using Base.DAL;
using Common;
using Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;
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

        public async Task<ActionResult> WrapListViewResult(IEnumerable data)
        {
            var canWrite = _appContext.IsAdmin() || _appContext.IsEditor();
            return Ok(new
            {
                Data = data,
                Create = canWrite,
                Update = canWrite,
                Delete = canWrite

            });
        }
    }
}
