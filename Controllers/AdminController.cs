using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Jobs2.Modules;
using Jobs2.Services;
using Jobs2.interfaces;

namespace Jobs2.Controllers
{
    [ApiController]
    [Route("user")]
    public class AdminController : ControllerBase
    {
        //public AdminController(){}
        private IUserService UserService;
        private ITokenService TokenService;

        private IJobService Jobs2Service;


        public AdminController(IUserService IUserService, ITokenService tokenService, IJobService Jobs2Service)
        {
            this.UserService = IUserService;
            this.TokenService = tokenService;
            this.Jobs2Service = Jobs2Service;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] User User)
        {
            // var dt = DateTime.Now;
            string type = "";
            User MyUser;

            if (User.UserName == "Miri&Shani"
            && User.Password == $"MSMB!")
            {
                type = "Admin";
            }
            else
            {
                 MyUser = UserService.GetUser(User.UserName, User.Password);
            if (MyUser!=null)
            {   
                type = "User";
                Jobs2Service.CurrentUser=MyUser;
            }
            else
            {
                return Unauthorized();
            }
            }
         

            var claims = new List<Claim>
            {
                new Claim("type", type),
                new Claim("name", User.UserName),
                new Claim("password", User.Password)
            };
               
            var token = TokenService.GetToken(claims);
            return new OkObjectResult(TokenService.WriteToken(token));
        }


        [HttpPost]
        [Route("[action]")]
        [Authorize(Policy = "Admin")]
        public IActionResult AddUser([FromBody] User User)
        {
            if(User.Password.Length<6 || User.Password.Length>12)
                 return BadRequest();
         return new OkObjectResult(UserService.postUser(User));
        }
/////////////??????????????????????????????????????????????????????????????לא עובד
        [HttpGet]
        [Authorize(Policy = "Admin")]
        //[Route("[action]")]
        public ActionResult<List<User>> GetAllUsers() 
        {
          return UserService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult<User> Get(int id)
        {
            var user = UserService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult DeleteUserAndAllHisJobs(int id)
        {
            var user = UserService.Get(id); 
            if (user is null)
            {
                return NotFound();
            }
            UserService.Delete(id);
            Jobs2Service.DeleteMyJobs(id);
            return NoContent();
        }
    }

}