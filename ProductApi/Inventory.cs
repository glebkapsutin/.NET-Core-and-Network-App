using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{

    public class Inventory
    {
        [Key]
        public int Id_Inventory{ get; set; }
        
        [Required]
        public int Id_Product { get; set; }

        [Required]

        public int StockQantity { get; set; }
        
        
    }
}
