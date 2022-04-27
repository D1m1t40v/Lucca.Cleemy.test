using ExpenseAPI.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace ExpenseAPI.Models
{
    public class CreateExpense
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [DateValidationRange]
        public DateTime Date { get; set; }

        [Required]
        public ExpenseNature Nature { get; set; }

        [Required]        
        public double Amount { get; set; }

        [Required]
        [StringLength(3)]        
        public string Currency { get; set; } = string.Empty;

        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}
