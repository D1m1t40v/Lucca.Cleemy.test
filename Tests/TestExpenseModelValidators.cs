using ExpenseAPI.Models.Validators;
using System;
using Xunit;

namespace Tests
{
    public class TestExpenseModelValidators
    {
        [Fact]        
        public void TestDateValidator()
        {
            var validator = new DateValidationRangeAttribute();

            var futureDate = DateTime.Now.AddDays(1);
            Assert.False(validator.IsValid(futureDate));

            var olderThan3MonthsDate = DateTime.Now.AddMonths(-3).AddDays(-1);
            Assert.False(validator.IsValid(olderThan3MonthsDate));
        }
    }
}
