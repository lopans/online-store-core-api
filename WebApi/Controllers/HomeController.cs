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
                    .Include(x => x.Category)
                    .GroupBy(x => new { x.Category.Link, x.Category.Title, x.Category.Icon })
                    .Select(x => new
                    {
                        Link = x.Key.Link,
                        Title = x.Key.Title,
                        x.Key.Icon,
                        SubItems = x.Select(z => new
                        {
                            z.Link,
                            z.Icon,
                            z.Title
                        })
                    }).ToListAsync();
                return Ok(all);
            }
        }
    }
}
