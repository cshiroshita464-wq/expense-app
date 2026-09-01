using Microsoft.AspNetCore.Mvc;
using ExpenseApp.Data;
using ExpenseApp.Models;
using ExpenseApp.Services;

namespace ExpenseApp.Controllers;

public class ExpenseController : Controller
{

    // 支出関連の処理に使用するExpenseServiceを保持する
    private readonly ExpenseService _expenseService;

    // DIでAppDbContextとExpenseServiceを受け取る
    public ExpenseController(ExpenseService expenseService)
    {
        // Controller内で使えるように保存
        _expenseService = expenseService;
    }

    //一覧画面
    public IActionResult Index(string? category)
    {
        // 今日の日付取得
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        // 今月の開始日作成
        DateOnly startOfMonth = new DateOnly(
            today.Year,
            today.Month,
            1
        );
        // 来月の開始日作成
        DateOnly startOfNextMonth = startOfMonth.AddMonths(1);

        // 今月の支出取得
        List<Expense> expenses = _expenseService.GetMonthlyExpenses(
            startOfMonth,
            startOfNextMonth
        );

        //カテゴリ絞込
        expenses = _expenseService.FilterByCategory(expenses, category);
        //カテゴリごとの合計取得
        List<CategoryTotalViewModel> categoryTotals = _expenseService.CalculateCategoryTotals(expenses);

        //合計金額取得
        decimal total = _expenseService.CalculateTotal(expenses);

        // 一覧画面に渡すデータをまとめる
        ExpenseIndexViewModel viewModel = new ExpenseIndexViewModel
        {
            Expenses = expenses,
            Total = total,
            Year = today.Year,
            Month = today.Month,
            CategoryTotals = categoryTotals
        };

        return View(viewModel);
    }

    //支出追加画面
    public IActionResult Create()
    {
        //初期値
        Expense expense = new Expense
        {
            Date = DateOnly.FromDateTime(DateTime.Today),
            //requiredにしているためカテゴリーとメモも設定する
            Category = "",
            Memo = ""
        };
    
        return View(expense);
    }

    //追加登録処理
    [HttpPost]
    public IActionResult Create(Expense expense)
    {
        _expenseService.CreateExpense(expense);
        //Indexにかえす
        return RedirectToAction("Index");
    }

    //編集画面
    public IActionResult Edit(int id)
    {
        Expense? expense = _expenseService.GetExpenseById(id);

        if (expense == null)
        {
            return NotFound();
        }

        return View(expense);
    }

    //編集更新
    [HttpPost]
    public IActionResult Edit(Expense expense)
    {
        _expenseService.UpdateExpense(expense);

        return RedirectToAction("Index");
    }

    //削除確認画面
    public IActionResult Delete(int id)
    {
        Expense? expense = _expenseService.GetExpenseById(id);

        if (expense == null)
        {
            return NotFound();
        }

        return View(expense);
    }

    //削除処理
    [HttpPost]
    public IActionResult Delete(Expense expense)
    {
        _expenseService.DeleteExpense(expense);

        return RedirectToAction("Index");
    }
}
