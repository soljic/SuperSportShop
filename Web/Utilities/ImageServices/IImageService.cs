using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace Web.Utilities.ImageServices
{
    public interface IImageService
    {
        Task<string> UploadImage(HttpPostedFileWrapper file);
       
    }
}