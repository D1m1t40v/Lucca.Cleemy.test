using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseAPI.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; } = string.Empty;

        [StringLength(20)]
        public string SurName { get; set; } = string.Empty;

        [StringLength(3)]
        public string Currency { get; set; } = string.Empty;
    }
}
