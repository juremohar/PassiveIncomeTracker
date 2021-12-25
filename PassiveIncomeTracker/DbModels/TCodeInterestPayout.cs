using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassiveIncomeTracker.DbModels
{
    [Table("code_interest_payout")]
    public class TCodeInterestPayout
    {
        [Key]
        [Column("id_interest_payout")]
        public int IdInterestPayout { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("title")]
        public string Title { get; set; }
    }
}
