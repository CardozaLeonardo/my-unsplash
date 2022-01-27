using Application.Actions.UserActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Actions.PhotoActions
{
    public class ReadPhotoWithUser
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Url { get; set; }
        public ReadUser User { get; set; }
    }
}
