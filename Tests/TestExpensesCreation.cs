using ExpenseAPI;
using ExpenseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Tests
{
    public class TestExpensesCreation
    {
        private readonly DataContext mockedContext;

        public TestExpensesCreation()
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            mockedContext = new DataContext(options);
            mockedContext.Database.EnsureCreated();
        }

        [Fact]
        public async void Test_NoDuplicatedExpenses()
        {
            var expense = new CreateExpense { UserId = 1, Amount = 100, Comment = "Comment", Currency = "USD", Date = DateTime.Now.AddDays(+1), Nature = ExpenseNature.Hotel };
            var controller = new ExpenseAPI.Controllers.ExpensesController(mockedContext);

            var firstPost = await controller.PostExpense(expense);

            Assert.NotNull(firstPost);
            Assert.IsType<CreatedAtActionResult>(firstPost.Result);

            var secondPost = await controller.PostExpense(expense);

            Assert.NotNull(secondPost);
            Assert.IsType<BadRequestObjectResult>(secondPost.Result);
        }

        [Fact]
        public async void Test_CurrencyShouldMatch()
        {
            var expense = new CreateExpense { UserId = 1, Amount = 100, Comment = "Comment", Currency = "USD", Date = DateTime.Now.AddDays(+1), Nature = ExpenseNature.Hotel };
            var controller = new ExpenseAPI.Controllers.ExpensesController(mockedContext);

            var firstPost = await controller.PostExpense(expense);

            Assert.NotNull(firstPost);
            Assert.IsType<CreatedAtActionResult>(firstPost.Result);

            expense.Currency = "EUR";
            var secondPost = await controller.PostExpense(expense);

            Assert.NotNull(secondPost);
            Assert.IsType<BadRequestObjectResult>(secondPost.Result);
        }
    }
}