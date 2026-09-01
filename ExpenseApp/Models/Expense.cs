using System.ComponentModel.DataAnnotations;

namespace ExpenseApp.Models;

public class Expense
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
    public decimal Amount { get; set; }
    public required string Category { get; set; }
    public required string Memo { get; set; }
    
}