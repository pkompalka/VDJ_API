using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VDJAPI.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Album { get; set; }

        public int Score { get; set; }

        public string LeadAuthor { get; set; }

        public string FeatureAuthor { get; set; }

        public string FullAuthor { get; set; }

        public string Cover { get; set; }
    }
}
