using System.Web;
using Microsoft.AspNetCore.Http;

namespace Web.Models.Dtos
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public HttpPostedFileWrapper  Picture { get; set; }
    }
}