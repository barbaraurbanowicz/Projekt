using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpendingTrackerAPI.Database;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Exceptions;

namespace SpendingTrackerAPI.Services;

public interface IExpenseService
{
    IEnumerable<Expense> GetAll();
    Expense GetById(int id);
    void Create(Expense expense);
    void Remove(int id);
    void Update(int id, Expense expense);
}

public class ExpenseService : IExpenseService
{
    private readonly AppDbContext _context;

    public ExpenseService(AppDbContext context)
    {
        _context = context;
    }
    
    
    public IEnumerable<Expense> GetAll()
    {
        IQueryable<Expense> expenseQuery = _context.Expenses.Include(x => x.ExpenseCategory);

        var expenses = expenseQuery.ToList();
        return expenses;
    }
    
    public Expense GetById(int id)
    {
        var expense = _context.Expenses.Include(x=>x.ExpenseCategory).FirstOrDefault(x => x.Id == id);
        if (expense == null) throw new NotFoundException("Not found!");
        return expense;
    }
    
    public void Create(Expense expense)
    {
        var newExpense = new Expense()
        {
            Name = expense.Name,
            Amount = expense.Amount,
            ExpenseCategoryId = expense.ExpenseCategoryId
        };
        _context.Expenses.Add(newExpense);
        _context.SaveChanges();
    }
    
    public void Remove(int id)
    {
        var expense = _context.Expenses.FirstOrDefault(x => x.Id == id);
        if (expense == null) throw new NotFoundException("Not found!");
        _context.Expenses.Remove(expense);
        _context.SaveChanges();
    }
    
    public void Update(int id, Expense expense)
    {
        var expenseDB = _context.Expenses.FirstOrDefault(x => x.Id == id);
        if (expenseDB == null) throw new NotFoundException("Not found!");
        expenseDB.Name = expense.Name;
        expenseDB.Amount = expense.Amount;
        expenseDB.ExpenseCategoryId = expense.ExpenseCategoryId;
        _context.SaveChanges();
    }
}