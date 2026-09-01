using ExpenseApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }
}