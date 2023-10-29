using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractsManager.Models
{
    public class ContractStage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ContractId { get; set; }

        [Required, MaxLength(128)]
        public string StageName { get; set; }

        [Required, Column(TypeName = "date")]
        public DateOnly StartDate;

        [Required, Column(TypeName = "date")]
        public DateOnly EndDate;
    }
}
