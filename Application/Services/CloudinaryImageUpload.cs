using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CloudinaryImageUpload : IUploadImageService
    {

        private Account Account;

        public CloudinaryImageUpload()
        {
            this.Account = new Account()
            {
                ApiKey = "875837363937192",
                ApiSecret = "1vf1Xz2NM66VTBBzB3y8nJX5AY8",
                Cloud = "dhksyvy7q"
            };
        }

        public string UploadImage(byte[] bytes)
        {

            var cloudinary = new Cloudinary(Account);

            string base64String = Convert.ToBase64String(bytes);
            var prefix = @"data:image/png;base64,";
            var imagePath = prefix + base64String;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imagePath),
                Folder = "MyUnsplash"
            };

            var uploadResult = cloudinary.Upload(@uploadParams);

            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}
