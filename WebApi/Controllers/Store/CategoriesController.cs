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
        public async Task<ActionResult> GetAll()
        {
            using (var uofw = CreateUnitOfWork)
            {

                var data = await _categoryService.GetAll(uofw).Select(x => new
                {
                    ID = x.ID,
                    Title = x.Title,
                    ImageID = x.Image != null ? (Guid?)x.Image.FileID : null,
                    FileName = x.Image != null ? x.Image.FileName + x.Image.Extension : null,
                    x.Description,
                    x.RowVersion
                }).ToListAsync();
                return await WrapListViewResult(data);
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult> Get(int id)
        {
            using (var uofw = CreateUnitOfWork)
            {

                var data = await _categoryService.GetAll(uofw)
                    .Where(x => x.ID == id)
                    .Select(x => new
                    {
                        ID = x.ID,
                        Title = x.Title,
                        x.Description,
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

                var ret = _categoryService.Update(uofw, new Category()
                {
                    Description = model.Description,
                    Title = model.Title,
                    ID = model.ID,
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
