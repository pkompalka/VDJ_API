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
    public class ChatsController : ControllerBase
    {
        private readonly ChatContext chatContext;

        public ChatsController(ChatContext context)
        {
            chatContext = context;
        }

        // GET: api/Chats/{int}
        [HttpGet]
        [Route("api/Chats/{id:int}")]
        public ActionResult<IEnumerable<string>> Get(int id)
        {
            List<string> messagesToSend = (from n in chatContext.Chats orderby n.Id ascending where n.Id > id select n.Message).ToList();
            return messagesToSend;
        }

        // PUT: api/Chats
        [HttpPut]
        [Route("api/Chats")]
        public ActionResult Put([FromBody] string message)
        {
            Chat chatToAdd = new Chat();
            int idToAdd = (from n in chatContext.Chats orderby n.Id descending select n.Id).FirstOrDefault();
            chatToAdd.Id = idToAdd + 1;
            chatToAdd.Message = message;
            chatContext.Chats.Add(chatToAdd);
            chatContext.SaveChanges();
            return Ok();
        }

        // POST api/Chats/clear
        [HttpPost]
        [Route("api/Chats/first")]
        public ActionResult PostAddDJ([FromBody] string firstChatMessage)
        {
            Chat firstChat = new Chat
            {
                Id = 1,
                Message = firstChatMessage
            };
            chatContext.Chats.Add(firstChat);
            chatContext.SaveChanges();
            return Ok();
        }

        // DELETE: api/Chats
        [HttpDelete]
        [Route("api/Chats")]
        public ActionResult Delete()
        {
            chatContext.Chats.RemoveRange(chatContext.Chats.ToList());
            chatContext.SaveChanges();
            return Ok();
        }
    }
}
