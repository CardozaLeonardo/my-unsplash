
using System.ComponentModel.DataAnnotations;

namespace Application.Actions.PhotoActions
{
    public class CreatePhoto
    {
        [Required]
        [MaxLength(100)]
        public string Label { get; set; }

        [Required]
        [MaxLength(250)]
        public string Url { get; set; } 
    }
}
