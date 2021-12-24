using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassiveIncomeTracker.DbModels
{
    [Table("users_interests")]
    public class TUserInterest
    {
        [Key]
        [Column("id_user_interest")]
        public int IdUserInterest { get; set; }

        [Column("id_user")]
        public int IdUser { get; set; }

        [Column("id_cryptocurrency")]
        public int IdCryptocurrency { get; set; }

        [Column("original_amount")]
        public double Amount { get; set; }

        [Column("compounded_amount")]
        public double? CompoundedAmount { get; set; }

        [Column("interest")]
        public double Interest { get; set; }

        [Column("note")]
        public string Note { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }

        [ForeignKey("IdUser")]
        public TUser User { get; set; }

        [ForeignKey("IdCryptocurrency")]
        public TCryptocurrency Cryptocurrency { get; set; }
    }
}
