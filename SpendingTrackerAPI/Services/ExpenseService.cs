using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpendingTrackerAPI.Database;
using SpendingTrackerAPI.DTOModels;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Exceptions;

namespace SpendingTrackerAPI.Services;

public interface IExpenseService
{
    /// <summary>
    ///  Get records Expense from table by ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    IEnumerable<Expense> GetAll();
    /// <summary>
    ///  Get record Expense from table by ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Expense GetById(int id);
    /// <summary>
    /// Create record Expense in table
    /// </summary>
    /// <param name="expenseCategory"></param>
    void Create(CreateExpenseDTO expense);
    /// <summary>
    /// Remove record  Expense from table
    /// </summary>
    /// <param name="id"></param>
    void Remove(int id);
    /// <summary>
    /// Update record Expense in table
    /// </summary>
    /// <param name="id"></param>
    /// <param name="expenseCategory"></param>
    void Update(int id, CreateExpenseDTO expense);
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
    
    public void Create(CreateExpenseDTO expense)
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
    
    public void Update(int id, CreateExpenseDTO expense)
    {
        var expenseDB = _context.Expenses.FirstOrDefault(x => x.Id == id);
        if (expenseDB == null) throw new NotFoundException("Not found!");
        expenseDB.Name = expense.Name;
        expenseDB.Amount = expense.Amount;
        expenseDB.ExpenseCategoryId = expense.ExpenseCategoryId;
        _context.SaveChanges();
    }
}