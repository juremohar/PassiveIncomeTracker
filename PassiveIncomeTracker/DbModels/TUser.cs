using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassiveIncomeTracker.DbModels
{
    [Table("users")]
    public class TUser
    {
        [Key]
        [Column("id_user")]
        public int IdUser { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
