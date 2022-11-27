using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassiveIncomeTracker.DbModels
{
    [Table("user_favourite_cryptocurrencies")]
    public class TUserFavouriteCryptocurrency
    {
        [Key]
        [Column("id_user_favourite_cryptocurrency")]
        public int IdUserFavouriteCryptocurrency { get; set; }

        [Column("id_user")]
        public int IdUser { get; set; }

        [Column("id_cryptocurrency")]
        public int IdCryptocurrency { get; set; }

        [Column("inserted_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime InsertedAt { get; set; }

        [ForeignKey("IdUser")]
        public TUser User { get; set; }

        [ForeignKey("IdCryptocurrency")]
        public TCryptocurrency Cryptocurrency { get; set; }

    }
}
