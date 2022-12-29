using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BurgerWebAPI.Model
{
    [Table("TBL_BURGER", Schema = "dbo")]
    public class Burger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("burger_id")]
        public int BurgerID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}
