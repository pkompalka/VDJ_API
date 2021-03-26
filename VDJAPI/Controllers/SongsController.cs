using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VDJAPI.Models;

namespace VDJAPI.Controllers
{
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly SongContext songContext;

        public SongsController(SongContext context)
        {
            songContext = context;
        }

        // GET: api/Songs/all
        [HttpGet]
        [Route("api/Songs/all")]
        public ActionResult<IEnumerable<Song>> Get()
        {
            List<Song> SongList = songContext.Songs.ToList();
            for (int i = 0; i < SongList.Count; i++)
            {
                SongList[i].Cover = "";
            }
            return SongList;
        }

        // GET: api/Songs/cover/{string}
        [HttpGet]
        [Route("api/Songs/cover/{idSongs}")]
        public ActionResult<string[]> GetCoverArt(string idSongs)
        {
            string[] songsIdArray = idSongs.Split(',');
            int coverNumber = songsIdArray.Length;
            string[] coversToSend = new string[coverNumber];

            for (int i = 0; i < coverNumber; i++)
            {
                int songId = int.Parse(songsIdArray[i]);
                Song song = songContext.Songs.Find(songId);
                coversToSend[i] = song.Cover;
            }
            return coversToSend;
        }

        // GET: api/Songs/votes
        [HttpGet]
        [Route("api/Songs/votes")]
        public ActionResult<IEnumerable<VotedSong>> GetVotes()
        {
            List<Song> topSongList = new List<Song>();
            topSongList = songContext.Songs.OrderByDescending(q => q.Score).ToList();
            List<VotedSong> votedSongList = new List<VotedSong>();
            for (int i = 0; i < 5; i++)
            {
                VotedSong votedSong = new VotedSong();
                votedSong.SongId = topSongList[i].Id;
                votedSong.Votes = topSongList[i].Score;
                votedSongList.Add(votedSong);
            }
            return votedSongList;
        }

        // GET: api/Songs/morevotes
        [HttpGet]
        [Route("api/Songs/morevotes")]
        public ActionResult<IEnumerable<VotedSong>> GetMoreVotes()
        {
            List<Song> topSongList = new List<Song>();
            topSongList = songContext.Songs.OrderByDescending(q => q.Score).ToList();
            List<VotedSong> votedSongList = new List<VotedSong>();
            for (int i = 0; i < 10; i++)
            {
                VotedSong votedSong = new VotedSong();
                votedSong.SongId = topSongList[i].Id;
                votedSong.Votes = topSongList[i].Score;
                votedSongList.Add(votedSong);
            }
            return votedSongList;
        }

        // PUT: api/Songs
        [HttpPut]
        [Route("api/Songs")]
        public ActionResult Put([FromBody] int songId)
        {
            Song song = songContext.Songs.Find(songId);

            if (songId == song.Id)
            {
                song.Score = song.Score + 1;
                songContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Songs
        [HttpPost]
        [Route("api/Songs/add")]
        public ActionResult Post([FromBody] List<Song> songList)
        {
            songContext.Songs.AddRange(songList);
            songContext.SaveChanges();
            return Ok();
        }

        // DELETE: api/Songs
        [HttpDelete]
        [Route("api/Songs")]
        public ActionResult Delete()
        {
            songContext.Songs.RemoveRange(songContext.Songs.ToList());
            songContext.SaveChanges();
            return Ok();
        }
    }
}
