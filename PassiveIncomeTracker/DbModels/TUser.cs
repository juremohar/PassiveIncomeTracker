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

        [Column("id_country")]
        public int IdCountry { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("salt")]
        public string Salt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("IdCountry")]
        public TCountry Country { get; set; }
    }
}
