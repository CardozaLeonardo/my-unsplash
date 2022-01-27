using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class DeletePhotoPassword
    {
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
