using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassiveIncomeTracker.DbModels
{
    [Table("cryptocurrencies")]
    public class TCryptocurrency
    {
        [Key]
        [Column("id_cryptocurrency")]
        public int IdCryptocurrency { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
