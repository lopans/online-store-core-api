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
    [Route("api/products")]
    [ApiController]
    public class ProductController : CoreControllerBase
    {
        private readonly IBaseService<Item> _productService;
        public ProductController(DataContext context, IApplicationContext appContext,
            IBaseService<Item> productService) : base(context, appContext)
        {
            _productService = productService;
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult> GetAll([FromQuery]FilterModel filter, string subCategoryLink)
        {
            using (var uofw = CreateUnitOfWork)
            {
                var all = _productService.GetAll(uofw)
                    .Include(x => x.SubCategory.Link)
                    .Where(x => x.SubCategory.Link == subCategoryLink);
                if (!string.IsNullOrEmpty(filter.search))
                    all = all.Where(x => x.Title.Contains(filter.search) || 
                    x.Description.Contains(filter.search));

                var projection = all.Select(x => new
                {
                    ID = x.ID,
                    Title = x.Title,
                    x.Link,
                    x.Price,
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

                var data = await _productService.GetAll(uofw)
                    .Where(x => x.Link == link)
                    .Select(x => new
                    {
                        ID = x.ID,
                        Title = x.Title,
                        x.Description,
                        x.Link,
                        x.Price,
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
        public async Task<ActionResult> Create(Item model)
        {
            using (var uofw = CreateUnitOfWork)
            {
                var ret = await _productService.CreateAsync(uofw,
                    new Item()
                    {
                        SubCategoryID = model.SubCategoryID,
                        Description = model.Description,
                        Title = model.Title,
                        Link = model.Link,
                        Price = model.Price,
                        ImageID = model.ImageID
                    });

                return Ok(ret);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("update")]
        public async Task<ActionResult> Update(Item model)
        {
            using (var uofw = CreateUnitOfWork)
            {
                var imgID = await _productService.GetAll(uofw)
                    .Where(x => x.ID == model.ID)
                    .Select(x => x.ImageID).SingleAsync();

                var ret = await _productService.Update(uofw, new Item()
                {
                    SubCategoryID = model.SubCategoryID,
                    Description = model.Description,
                    Title = model.Title,
                    Price = model.Price,
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
                _productService.Delete(uofw, id);
                return Ok();
            }
        }
    }
}
