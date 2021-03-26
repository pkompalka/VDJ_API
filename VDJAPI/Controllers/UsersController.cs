using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VDJAPI.Models;

namespace VDJAPI.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext userContext;

        public UsersController(UserContext context)
        {
            userContext = context;
        }

        // GET: api/Users/all
        [HttpGet]
        [Route("api/Users/all")]
        public ActionResult<IEnumerable<string>> Get()
        {
            List<string> nameList = new List<string>();
            List<User> userList = userContext.Users.ToList();

            for (int i = 0; i < userContext.Users.Count(); i++)
            {
                nameList.Add(userList[i].Nick);
            }

            return nameList;
        }

        // GET: api/Users/password
        [HttpGet]
        [Route("api/Users/password")]
        public ActionResult<string> GetPassword()
        {
            List<User> userList = userContext.Users.ToList();
            User dj = userList.Find(x => x.Id == 1);
            string password = dj.Password;
            return password;
        }

        // GET: api/Users/number
        [HttpGet]
        [Route("api/Users/number")]
        public ActionResult<int> GetNumber()
        {
            return userContext.Users.ToList().Count - 1;
        }

        // POST api/Users/adduser
        [HttpPost]
        [Route("api/Users/adduser")]
        public ActionResult<int> Post([FromBody] string name)
        {
            List<User> userList = userContext.Users.ToList();

            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].Nick.ToLower() == name.ToLower())
                {
                    return 0;
                }
            }

            User user = new User();
            user.Nick = name;
            user.Password = "";
            int idToAdd = (from n in userContext.Users orderby n.Id descending select n.Id).FirstOrDefault();
            user.Id = idToAdd + 1;
            userContext.Users.Add(user);
            userContext.SaveChanges();
            return 1;
        }

        // POST api/Users/adddj
        [HttpPost]
        [Route("api/Users/adddj")]
        public ActionResult PostAddDJ([FromBody] string password)
        {
            User user = new User();
            user.Id = 1;
            user.Nick = "DJ";
            user.Password = password;
            userContext.Users.Add(user);
            userContext.SaveChanges();
            return Ok();
        }

        // PUT api/Users/close
        [HttpPut]
        [Route("api/Users/close")]
        public ActionResult Put([FromBody] string password)
        {
            userContext.Users.Find(1).Password = password;
            userContext.SaveChanges();
            return Ok();
        }

        // DELETE: api/Users
        [HttpDelete]
        [Route("api/Users")]
        public ActionResult Delete()
        {
            userContext.Users.RemoveRange(userContext.Users.ToList());
            userContext.SaveChanges();
            return Ok();
        }
    }
}
