using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VDJAPI.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        public int SongId { get; set; }

        public string Nick { get; set; }

        public string Dedication { get; set; }
    }
}
