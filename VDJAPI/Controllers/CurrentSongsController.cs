using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VDJAPI.Models;

namespace VDJAPI.Controllers
{
    [ApiController]
    public class CurrentSongsController : ControllerBase
    {
        private readonly CurrentSongContext currentSongContext;

        private readonly PlaylistContext playlistContext;

        public CurrentSongsController(CurrentSongContext context1, PlaylistContext context2)
        {
            currentSongContext = context1;
            playlistContext = context2;
        }

        // GET: api/CurrentSongs
        [HttpGet]
        [Route("api/CurrentSongs")]
        public IEnumerable<int> Get()
        {
            List<int> currentSongIdList = (from n in currentSongContext.CurrentSongs orderby n.Id select n.SongId).ToList();
            List<int> nextSongIdList = (from n in playlistContext.Playlist orderby n.Id where n.WasPlayed == false select n.SongId).ToList();
            if(currentSongIdList.Count == 0)
            {
                return new int[] { -1, -1 };
            }
            if(nextSongIdList.Count == 0)
            {
                return new int[] { currentSongIdList.First(), -1 };
            }
            return new int[] { currentSongIdList.First(), nextSongIdList.First() };
        }

        // POST: api/CurrentSongs/add
        [HttpPost]
        [Route("api/CurrentSongs/add")]
        public ActionResult Post([FromBody] int songId)
        {
            CurrentSong songToAdd = new CurrentSong
            {
                Id = 1,
                SongId = songId
            };
            currentSongContext.CurrentSongs.Add(songToAdd);
            currentSongContext.SaveChanges();
            return Ok();
        }

        // DELETE: api/CurrentSongs
        [HttpDelete]
        [Route("api/CurrentSongs")]
        public ActionResult Delete()
        {
            currentSongContext.CurrentSongs.RemoveRange(currentSongContext.CurrentSongs.ToList());
            currentSongContext.SaveChanges();
            return Ok();
        }
    }
}
