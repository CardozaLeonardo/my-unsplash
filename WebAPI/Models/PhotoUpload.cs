using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class PhotoUpload
    {
        [Required]
        public string Label { get; set; }
        
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
