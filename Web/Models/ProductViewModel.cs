using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name must not exceed 100 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product category is required.")]
        [StringLength(50, ErrorMessage = "Product category must not exceed 50 characters.")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        [Range(0.01, 999999999999.99, ErrorMessage = "Product price must be between 0.01 and 999999999999.99.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [StringLength(50, ErrorMessage = "User ID must not exceed 50 characters.")]
        [Display(Name = "User ID")]
        public string UserId { get; set; }
    }
}