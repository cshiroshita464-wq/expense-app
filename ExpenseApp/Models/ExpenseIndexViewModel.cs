namespace ExpenseApp.Models;

public class ExpenseIndexViewModel
{
    public List<Expense> Expenses { get; set; } = new();

    public decimal Total { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }
    public List<CategoryTotalViewModel> CategoryTotals { get; set; } = new();
}