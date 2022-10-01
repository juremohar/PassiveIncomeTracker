using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassiveIncomeTracker.DbModels
{
    [Table("services")]
    public class TService
    {
        [Key]
        [Column("id_service")]
        public int IdService { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }
    }
}
