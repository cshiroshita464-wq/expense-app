using Microsoft.AspNetCore.Mvc;
using ExpenseApp.Models;
using ExpenseApp.Services;

namespace ExpenseApp.Controllers;

[ApiController]
[Route("api/expenses")]
public class ExpenseApiController : ControllerBase
{
    private readonly ExpenseService _expenseService;

    public ExpenseApiController(ExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public IActionResult GetExpenses()
    {
        List<Expense> expenses = _expenseService.GetAllExpenses();

        // ExpenseをExpenseDtoに変換
        List<ExpenseDto> expenseDtos = expenses
            .Select(expense => new ExpenseDto
            {
                Id = expense.Id,
                Date = expense.Date,
                Amount = expense.Amount,
                Category = expense.Category,
                Memo = expense.Memo
            })
            .ToList();

        return Ok(expenseDtos);
    }

    [HttpPost]
    public IActionResult PostExpenses(CreateExpenseDto dto)
    {
        Expense expense = new Expense
            {
                Date = dto.Date,
                Amount = dto.Amount,
                Category = dto.Category,
                Memo = dto.Memo
            };
        _expenseService.CreateExpense(expense);
        
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult PutExpense(int id, CreateExpenseDto dto)
    {
        Expense? expense = _expenseService.GetExpenseById(id);

        //存在しない場合
        if (expense == null)
        {
            return NotFound();
        }

        expense.Date = dto.Date;
        expense.Amount = dto.Amount;
        expense.Category = dto.Category;
        expense.Memo = dto.Memo;

        _expenseService.UpdateExpense(expense);

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteExpense(int id)
    {
        Expense? expense = _expenseService.GetExpenseById(id);

        if (expense == null)
        {
            return NotFound();
        }
        _expenseService.DeleteExpense(expense);
        return Ok();
    }
}