using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VDJAPI.Models;

namespace VDJAPI.Controllers
{
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly PlaylistContext playlistContext;

        public PlaylistsController(PlaylistContext context)
        {
            playlistContext = context;
        }

        // GET: api/Playlists
        [HttpGet]
        [Route("api/Playlists")]
        public ActionResult<IEnumerable<Playlist>> Get()
        {
            return playlistContext.Playlist.ToList();
        }

        // POST: api/Playlists/add
        [HttpPost]
        [Route("api/Playlists/add")]
        public ActionResult Post([FromBody] List<Playlist> playlistList)
        {
            if(playlistList[0].Id == 1)
            {

            }
            else
            {
                List<Playlist> playedSongsList = new List<Playlist>();
                for (int i = 0; i < playlistContext.Playlist.ToList().Count; i++)
                {
                    playedSongsList.Add(playlistContext.Playlist.ToList()[i]);
                    if (playlistContext.Playlist.ToList()[i].WasPlayed == false)
                    {
                        break;
                    }
                }
                playedSongsList.AddRange(playlistList);
            }

            playlistContext.Playlist.RemoveRange(playlistContext.Playlist.ToList());
            playlistContext.Playlist.AddRange(playlistList);
            playlistContext.SaveChanges();
            return Ok();
        }

        // PUT: api/Playlists
        [HttpPut]
        [Route("api/Playlists/update")]
        public ActionResult Put([FromBody] int playlistId)
        {
            int databaseId = playlistId + 1;
            playlistContext.Playlist.Find(databaseId).WasPlayed = true;
            playlistContext.SaveChanges();
            return Ok();
        }

        // DELETE: api/Playlists
        [HttpDelete]
        [Route("api/Playlists")]
        public ActionResult Delete()
        {
            playlistContext.Playlist.RemoveRange(playlistContext.Playlist.ToList());
            playlistContext.SaveChanges();
            return Ok();
        }
    }
}
