using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DrinkWebAPI.Model
{
    [Table("TBL_DRINK")]
    public class Drink
    {
        [Key]
        [Column("drink_id")]
        public int DrinkID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}
