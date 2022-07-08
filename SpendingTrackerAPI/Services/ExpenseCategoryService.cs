using SpendingTrackerAPI.Database;
using SpendingTrackerAPI.DTOModels;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Exceptions;

namespace SpendingTrackerAPI.Services;

public interface IExpenseCategoryService
{
    /// <summary>
    ///  Get record Expense Category from table by ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ExpenseCategory GetById(int id);
    /// <summary>
    /// Get all Expense Category records from table
    /// </summary>
    /// <returns></returns>
    IEnumerable<ExpenseCategory> GetAll();
    
    /// <summary>
    /// Create record Expense Category in table
    /// </summary>
    /// <param name="expenseCategory"></param>
    void Create(CreateExpenseCategoryDTO expenseCategory);
    /// <summary>
    /// Update record Expense Category in table
    /// </summary>
    /// <param name="id"></param>
    /// <param name="expenseCategory"></param>
    void Update(int id ,CreateExpenseCategoryDTO expenseCategory);
    /// <summary>
    /// Remove record Expense Category from table
    /// </summary>
    /// <param name="id"></param>
    void Remove(int id);
}

public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly AppDbContext _context;

    public ExpenseCategoryService(AppDbContext context)
    {
        _context = context;
    }
    
    public ExpenseCategory GetById(int id)
    {
        var category = _context.ExpenseCategories.FirstOrDefault(x => x.Id == id);
        if (category == null) throw new NotFoundException("Not found!");
        return category;
    }
    
    public IEnumerable<ExpenseCategory> GetAll()
    {
        var categories = _context.ExpenseCategories.ToList();
        return categories;
    }
    
    public void Create(CreateExpenseCategoryDTO expenseCategory)
    {
        var newCategory = new ExpenseCategory()
        {
            Name = expenseCategory.Name,
        };

        _context.ExpenseCategories.Add(newCategory);
        _context.SaveChanges();
    }
        
    public void Update(int id ,CreateExpenseCategoryDTO expenseCategory)
    {
        var dbCategory = _context.ExpenseCategories.FirstOrDefault(c => c.Id == id);
        if (dbCategory == null) throw new NotFoundException("Not found!");
        dbCategory.Name = expenseCategory.Name;
        _context.SaveChanges();
    }
    
    public void Remove(int id)
    {
        var category = _context.ExpenseCategories.FirstOrDefault(c => c.Id == id);
        if (category == null) throw new NotFoundException("Not found!");
        _context.ExpenseCategories.Remove(category);
        _context.SaveChanges();
    }
}