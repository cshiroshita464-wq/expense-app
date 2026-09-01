namespace ExpenseApp.Models;

public class ExpenseDto
{
    //テストのため一時的にIDついか
    public int Id { get; set; }
    public DateOnly Date { get; set; }

    public decimal Amount { get; set; }

    public string Category { get; set; } = "";
    public string Memo { get; set; } = "";
}