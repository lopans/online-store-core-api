using Base.Services;
using Common;
using Data;
using Data.Entities.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Store;

namespace WebApi.Controllers.Store
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : CoreControllerBase
    {
        private readonly IBaseService<SubCategory> _subCategoryService;
        public HomeController(DataContext context, IApplicationContext appContext,
            IBaseService<SubCategory> subCategoryService) : base(context, appContext)
        {
            _subCategoryService = subCategoryService;
        }
        [HttpGet]
        [Route("getMenu")]
        public async Task<ActionResult> GetMenu()
        {
            using (var uofw = CreateUnitOfWork)
            {
                var all = await _subCategoryService.GetAll(uofw)
                    .GroupBy(x => new { x.Category.ID, x.Category.Title })
                    .Select(x => new
                    {
                        ID = x.Key.ID,
                        Title = x.Key.Title,
                        SubItems = x.Select(z => new
                        {
                            z.ID,
                            z.Title
                        })
                    }).ToListAsync();
                return Ok(all);
            }
        }
    }
}
