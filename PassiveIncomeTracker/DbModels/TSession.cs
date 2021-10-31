using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassiveIncomeTracker.DbModels
{
    [Table("sessions")]
    public class TSession
    {
        [Key]
        [Column("id_session")]
        public int IdSession { get; set; }

        [Column("id_user")]
        public int IdUser { get; set; }

        [Column("token")]
        public string Token { get; set; }

        [Column("revoked_at")]
        public DateTime? RevokedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }
    }
}
