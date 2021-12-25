using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassiveIncomeTracker.DbModels
{
    [Table("users_realized_interests")]
    public class TUserRealizedInterest
    {
        [Key]
        [Column("id_user_realized_interest")]
        public int IdUserRealizedInterest { get; set; }

        [Column("id_user")]
        public int IdUser { get; set; }

        [Column("id_user_interest")]
        public int IdUserInterest { get; set; }

        [Column("total_amount")]
        public double TotalAmount { get; set; }

        [Column("gained_amount")]
        public double GainedAmount { get; set; }

        [Column("interest")]
        public double Interest { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }

        [ForeignKey("IdUser")]
        public TUser User { get; set; }

        [ForeignKey("IdUserInterest")]
        public TUserInterest UserInterest { get; set; }
    }
}
