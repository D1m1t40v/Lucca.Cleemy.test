namespace ExpenseAPI.Models
{
    public class DisplayExpense
    {
        public string UserName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public ExpenseNature Nature { get; set; }

        public double Amount { get; set; }

        public string Currency { get; set; } = string.Empty;

        public string Comment { get; set; } = string.Empty;
    }
}
