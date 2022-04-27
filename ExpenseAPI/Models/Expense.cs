using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseAPI.Models
{
    public class Expense
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime Date { get; set; }

        public ExpenseNature Nature { get; set; }

        public double Amount { get; set; }

        [StringLength(3)]
        public string Currency { get; set; } = string.Empty;

        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}
