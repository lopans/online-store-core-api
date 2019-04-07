using Base.DAL;
using Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [EnableCors("any")]
    public class CoreControllerBase: ControllerBase
    {
        public CoreControllerBase(DataContext context)
        {

        }
        private IUnitOfWork uofw;
        private ISystemUnitOfWork suofw;
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
    }
}
