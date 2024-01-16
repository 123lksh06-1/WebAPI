using System.ComponentModel.DataAnnotations;
using WebAPI.Model.Validations;

namespace WebAPI.Model
{
    public class Shirt
    {
        public int ShirtId { get; set; }
        [Required]
        public string? Brand { get; set; } //null type therefore using question mark
        [Required]
        public string? Color { get; set; }
        [Shirt_EnsureCorrectSizing]
        public int Size { get; set; }
        [Required]
        public string? Gender { get; set; }
        public double Price { get; set; }


    }
}
