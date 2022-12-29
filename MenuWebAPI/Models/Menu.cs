using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MenuWebAPI.Model
{
    [Table("TBL_MENU", Schema = "dbo")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("menu_id")]
        public int MenuID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}
