using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassiveIncomeTracker.DbModels
{
    [Table("countries")]
    public class TCountry
    {
        [Key]
        [Column("id_country")]
        public int IdCountry { get; set; }

        [Column("short")]
        public string Short { get; set; }

        [Column("name")]
        public string Name { get; set; }

    }
}
