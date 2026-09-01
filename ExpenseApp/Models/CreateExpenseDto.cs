namespace ExpenseApp.Models;

public class CreateExpenseDto
{
    public DateOnly Date { get; set; }

    public decimal Amount { get; set; }

    public string Category { get; set; } = "";

    public string Memo { get; set;} = "";
}