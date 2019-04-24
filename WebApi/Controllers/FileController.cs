using Base.DAL;
using Base.Services;
using Base.Services.Media;
using Common;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : CoreControllerBase
    {
        private readonly IFileSystemService _fileSystemService;
        public FileController(DataContext context, 
            IApplicationContext appContext,
            IFileSystemService fileSystemService)
            :base(context, appContext)
        {
            _fileSystemService = fileSystemService;
        }
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetFile(string fileid)
        {
            if (fileid == null)
                throw new FileNotFoundException();
            int id = 0;
            Guid guid = Guid.Empty;
            if (!Guid.TryParse(fileid, out guid))
                if (!int.TryParse(fileid, out id))
                    throw new FileNotFoundException();

            using (var suofw = CreateSystemUnitOfWork)
            {
                Stream stream;
                var ms = new MemoryStream();
                try
                {
                    if (id > 0)
                        guid = (await suofw.GetRepository<FileData>().All()
                            .Where(x => x.ID == id)
                            .SingleOrDefaultAsync())?.FileID ?? Guid.Empty;
                    if (guid == Guid.Empty)
                        throw new FileNotFoundException();
                    using (stream = await _fileSystemService.GetFile(guid, suofw))
                    {
                        await stream.CopyToAsync(ms);
                        stream.Flush();
                    }
                }
                catch (FileNotFoundException)
                {
                    using (stream = System.IO.File.Open(_fileSystemService.DefaultImagePath, FileMode.Open))
                    {
                        await stream.CopyToAsync(ms);
                        stream.Flush();
                    }
                }
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms, "application/octet-stream");
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult> UploadFile()
        {
            var file = Request.Form.Files[0];
            if(file == null || file.Length == 0)
                return BadRequest();

            var provider = new MultipartMemoryStreamProvider();
            var filename = file.FileName.Trim('\"');
            var ext = "." + filename.Split('.').Last();
            using(var stream = file.OpenReadStream())
            {
                FileData ret = new FileData();
                try
                {
                    using (var uofw = CreateUnitOfWork)
                    {
                        ret = await _fileSystemService.SaveFile(stream, uofw, filename, ext);
                    }
                    return Ok(ret.ID);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
        }
    }
}
