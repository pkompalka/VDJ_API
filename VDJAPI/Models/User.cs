using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VDJAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Nick { get; set; }

        public string Password { get; set; }
    }
}
