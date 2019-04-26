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
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : CoreControllerBase
    {
        private readonly IBaseService<Category> _categoryService;
        public CategoriesController(DataContext context, IApplicationContext appContext,
            IBaseService<Category> categoryService) : base(context, appContext)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult> GetAll([FromQuery]FilterModel filter)
        {
            using (var uofw = CreateUnitOfWork)
            {
                var all = _categoryService.GetAll(uofw);
                if (!string.IsNullOrEmpty(filter.search))
                    all = all.Where(x => x.Title.Contains(filter.search) || 
                    x.Description.Contains(filter.search));

                var projection = all.Select(x => new
                {
                    ID = x.ID,
                    Title = x.Title,
                    x.Link,
                    x.Icon,
                    ImageID = x.Image != null ? (Guid?)x.Image.FileID : null,
                    FileName = x.Image != null ? x.Image.FileName + x.Image.Extension : null,
                    x.Description,
                    x.RowVersion
                });
                return await WrapListViewResult(new ListViewWrapModel(projection, filter));
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult> Get(string link)
        {
            using (var uofw = CreateUnitOfWork)
            {

                var data = await _categoryService.GetAll(uofw)
                    .Where(x => x.Link == link)
                    .Select(x => new
                    {
                        ID = x.ID,
                        Title = x.Title,
                        x.Description,
                        x.Link,
                        x.Icon,
                        ImageID = x.Image != null ? (Guid?)x.Image.FileID : null,
                        FileName = x.Image != null ? x.Image.FileName + x.Image.Extension : null,
                        x.RowVersion
                    }).SingleOrDefaultAsync();
                if (data == null)
                    return NotFound();
                return Ok(data);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        public async Task<ActionResult> Create(CreateModel model)
        {
            using (var uofw = CreateUnitOfWork)
            {
                var ret = await _categoryService.CreateAsync(uofw,
                    new Category()
                    {
                        Description = model.Description,
                        Title = model.Title,
                        Icon = model.Icon,
                        Link = model.Link,
                        ImageID = model.ImageID
                    });

                return Ok(ret);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("update")]
        public async Task<ActionResult> Update(Category model)
        {
            using (var uofw = CreateUnitOfWork)
            {
                var imgID = await _categoryService.GetAll(uofw)
                    .Where(x => x.ID == model.ID)
                    .Select(x => x.ImageID).SingleAsync();

                var ret = await _categoryService.Update(uofw, new Category()
                {
                    Description = model.Description,
                    Title = model.Title,
                    Icon = model.Icon,
                    ID = model.ID,
                    Link = model.Link,
                    RowVersion = model.RowVersion,
                    ImageID = model.ImageID ?? imgID
                });
                return Ok(ret);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("delete")]
        public async Task<ActionResult> Delete(int id)
        {
            using (var uofw = CreateUnitOfWork)
            {
                _categoryService.Delete(uofw, id);
                return Ok();
            }
        }
    }
}
