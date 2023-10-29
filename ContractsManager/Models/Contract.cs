using System.ComponentModel.DataAnnotations;

namespace ContractsManager.Models
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string Code { get; set; }

        [Required, MaxLength(128)]
        public string Name { get; set; }

        [Required, MaxLength(128)]
        public string Customer { get; set; }

        public List<ContractStage> Stages { get; set; }
        public Contract()
        {
            Stages = new List<ContractStage>();
        }
    }
}
