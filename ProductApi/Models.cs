using System.ComponentModel.DataAnnotations; // Для валидации данных

namespace ProductApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; } = string.Empty; // Устанавливаем значение по умолчанию

        [Range(0, double.MaxValue)]
        public double Price { get; set; } = 0;
    }
}
