using Base.Identity.Entities;
using Base.Utils;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Account;

namespace WebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : CoreControllerBase
    {
        private readonly UserManager<User> _userManager;
        public AccountController(DataContext context, UserManager<User> userManager)
            :base(context)
        {
            _userManager = userManager;
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (model == null)
                throw new Exception("invalid data");
            if (!model.Email.IsValidEmailString())
                throw new Exception("invalid email address");
            var res = await _userManager.CreateAsync(new User()
            {
                Email = model.Email,
                UserName = model.Email
            }, 
            model.Password);
            if(res.Succeeded)
                return Ok("registered successfully");
            throw new Exception(res.Errors.First().Description);
        }

        [HttpGet]
        //[Authorize]
        [Route("getUserInfo")]
        public async Task<ActionResult> GetUserInfo()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Ok(new
            {
                user.UserName
            });
        }

        [HttpGet]
        [Authorize]
        [Route("authed")]
        public async Task<ActionResult> Authorized()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Ok(new
            {
                user.UserName
            });
        }
    }
}
