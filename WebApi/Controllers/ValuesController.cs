using Base.Services;
using Common;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : CoreControllerBase
    {
        private readonly IBaseService<TestObject> _service;
        public ValuesController(IBaseService<TestObject> service, DataContext context, IApplicationContext appContext)
            :base(context, appContext)
        {
            _service = service;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            using(var uofw = CreateUnitOfWork)
            {
                var t = await _service.CreateAsync(uofw, new TestObject() { Number = 15 });
                return new string[] { "value1", "value2" };
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        //[HttpGet]
        //[Authorize]
        //public async Task<ActionResult> GetSecret()
        //{
        //    return Ok("хуй");
        //}
    }
}
