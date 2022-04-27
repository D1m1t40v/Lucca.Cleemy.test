#nullable disable
using ExpenseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseAPI.Controllers
{
    //httprepl https://localhost:7071

    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly DataContext _context;

        public ExpensesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisplayExpense>>> GetExpenses(string sort = "")
        {
            var expenses = _context.Expenses.AsQueryable<Expense>();

            if(!string.IsNullOrEmpty(sort))
            {
                string[] sortingParams = sort.Split(':');
                expenses = SortExpenses(sortingParams, expenses);
            }

            return await expenses.Include(x => x.User).Select(e => ConvertToDisplayExpense(e)).ToListAsync();
        }

        // GET: api/Expenses/ByUser/5
        [HttpGet("/ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<DisplayExpense>>> GetUserExpenses(int userId, string sort = "")
        {
            var expenses = _context.Expenses.Where(e => e.UserId == userId);

            if (!string.IsNullOrEmpty(sort))
            {
                string[] sortingParams = sort.Split(':');
                expenses = SortExpenses(sortingParams, expenses);
            }

            return await expenses.Include(x => x.User).Select(e => ConvertToDisplayExpense(e)).ToListAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DisplayExpense>> GetExpense(int id)
        {
            var expense = await _context.Expenses.Include(x => x.User).FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            return ConvertToDisplayExpense(expense);
        }

        // POST: api/Expenses
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(CreateExpense createExpense)
        {
            Expense expense = ConvertToExpense(createExpense);

            if (ExpenseExists(expense))
            {
                return BadRequest("Cannot create a duplicated expense");
            }
            
            User currentUser = _context.Users.Find(expense.UserId);
            if(expense.Currency.ToUpper() != currentUser.Currency.ToUpper())
            {
                return BadRequest("Currencies are not compatible");
            }

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        private bool ExpenseExists(Expense expense)
        {
            return (_context.Expenses.Any(e => e.User == expense.User && e.Amount == expense.Amount && e.Date == expense.Date));
        }

        private static DisplayExpense ConvertToDisplayExpense(Expense fromDB)
        {
            return new DisplayExpense
            {                
                UserName = fromDB.User.SurName + " " + fromDB.User.Name,
                Date = fromDB.Date,
                Nature = fromDB.Nature,
                Amount = fromDB.Amount,
                Currency = fromDB.Currency,
                Comment = fromDB.Comment
            };
        }

        private Expense ConvertToExpense(CreateExpense fromAPI)
        {
            return new Expense
            {
                UserId = fromAPI.UserId,
                User = _context.Users.Find(fromAPI.UserId),
                Date = fromAPI.Date,
                Nature = fromAPI.Nature,
                Amount = fromAPI.Amount,
                Currency = fromAPI.Currency,
                Comment = fromAPI.Comment
            };
        }

        private IQueryable<Expense> SortExpenses(string[] sortingParams, IQueryable<Expense> expenses)
        {
            bool isAscending = true;
            switch (sortingParams[1])
            {
                case "asc":
                    isAscending = true;
                    break;
                case "desc":
                    isAscending = false;
                    break;
                default:
                    // we assume there is an error in the sorting query, continue
                    return expenses;
            }

            if (sortingParams[0].Contains("amount"))
            {
                if (isAscending)
                {
                    expenses = expenses.OrderBy(e => e.Amount);
                }
                else
                {
                    expenses = expenses.OrderByDescending(e => e.Amount);
                }
            }

            if (sortingParams[0].Contains("date"))
            {
                if (isAscending)
                {
                    expenses = expenses.OrderBy(e => e.Date);
                }
                else
                {
                    expenses = expenses.OrderByDescending(e => e.Date);
                }
            }

            return expenses;
        }
    }
}
