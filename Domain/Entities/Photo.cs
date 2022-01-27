using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Photo: Base
    {
        public string Label { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
