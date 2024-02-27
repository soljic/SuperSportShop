using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Web.Utilities.ImageServices
{
    public class ImageService : IImageService
    {
        private Cloudinary _cloudinary;

        public ImageService()
        {
            Account account = new Account(
                ConfigurationManager.AppSettings["CloudinaryCloudName"],
                ConfigurationManager.AppSettings["CloudinaryApiKey"],
                ConfigurationManager.AppSettings["CloudinaryApiSecret"]);

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImage(HttpPostedFileWrapper file)
        {
            if (file != null && file.ContentLength >  0)
            {
                using (var stream = file.InputStream)
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.Error != null)
                    {
                        throw new Exception(uploadResult.Error.Message);
                    }

                    return uploadResult.SecureUrl.ToString();
                }
            }

            return null;
        }
        
    }
}
