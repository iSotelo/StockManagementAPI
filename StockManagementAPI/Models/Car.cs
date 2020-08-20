using System.ComponentModel.DataAnnotations;

namespace StockManagementAPI.Models
{
    public class Car
    {
        [Key]
        [Required]
        public int CarId { get; set; }


        [Required(ErrorMessage = "The field is requiered")]
        [StringLength(50, ErrorMessage="The maximum length allowed is 50 characters")]
        public string Model { get; set; }


        [StringLength(100, ErrorMessage = "The maximum length allowed is 100 characters")]
        public string Description { get; set; }


        [Range(0, 9000, ErrorMessage = "The value must be in range 0 to 9000")]
        public int Year { get; set; }

        [Required(ErrorMessage = "The field is requiered")]
        [StringLength(50, ErrorMessage = "The maximum length allowed is 50 characters")]
        public string Brand { get; set; }


        [Required(ErrorMessage = "The field is requiered")]
        [Range(0, 1000000, ErrorMessage = "The value must be in range 0 to 1000000")]
        public int Kilometers { get; set; }


        [Required(ErrorMessage = "The field is requiered")]
        [Range(0, 10000000, ErrorMessage = "The value must be in range 0 to 10000000")]
        public decimal Price { get; set; }


        public string ImageUrl { get; set; }
    }
}
