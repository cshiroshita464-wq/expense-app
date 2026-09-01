using ExpenseApp.Data;
using ExpenseApp.Models;

namespace ExpenseApp.Services;

public class ExpenseService
{
    // DB操作に使用するAppDbContext
    private readonly AppDbContext _context;

    // DIでAppDbContextを受け取る
    public ExpenseService(AppDbContext context)
    {
        _context = context;
    }

    //Indexの処理
    //今月の支出を取得
    public List<Expense> GetMonthlyExpenses(DateOnly startOfMonth, DateOnly startOfNextMonth)
    {
        List<Expense> expenses = _context.Expenses
            .Where(expense =>
                expense.Date >= startOfMonth &&
                expense.Date < startOfNextMonth)
            .ToList();

        return expenses;
    }

    //絞り込み(カテゴリー)
    public List<Expense> FilterByCategory(List<Expense> expenses, string? category)
    {
        //検索ワードがなければそのまま返す
        if (string.IsNullOrEmpty(category))
        {
            return expenses;
        }
        //検索ワードで絞り込んで返す
        return expenses
            .Where(expense => expense.Category == category)
            .ToList();
    }

    //合計金額計算
    public decimal CalculateTotal(List<Expense> expenses)
    {
        return expenses
                .Sum(expense => expense.Amount);
    }

    //カテゴリーごとの合計
    public List<CategoryTotalViewModel> CalculateCategoryTotals(List<Expense> expenses)

    {
        return expenses
            .GroupBy(expense => expense.Category)
            .Select(group => new CategoryTotalViewModel
            {
                Category = group.Key,
                Total = group.Sum(expense => expense.Amount)
            })
            .ToList();

    }

    //Createの処理
    public void CreateExpense(Expense expense)
    {
        _context.Expenses.Add(expense);
        _context.SaveChanges();
    }

    
    // IDから支出を1件取得
    public Expense? GetExpenseById(int id)
    {
        return _context.Expenses.Find(id);
    }

    //Editの処理
    public void UpdateExpense(Expense expense)
    {
        _context.Expenses.Update(expense);
        _context.SaveChanges();
    }

    //Deleteの処理
    public void DeleteExpense(Expense expense)
    {
        _context.Expenses.Remove(expense);
        _context.SaveChanges();
    }

    //全件取得
    public List<Expense> GetAllExpenses()
    {
        return _context.Expenses.ToList();
    }
}