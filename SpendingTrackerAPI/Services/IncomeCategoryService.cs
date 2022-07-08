using Microsoft.EntityFrameworkCore;
using SpendingTrackerAPI.Database;
using SpendingTrackerAPI.DTOModels;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Exceptions;

namespace SpendingTrackerAPI.Services;

public interface IIncomeCategoryService
{
    /// <summary>
    ///  Get record Income Category from table by ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    IncomeCategory GetById(int id);
    /// <summary>
    ///  Get records Income Category from table by ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    IEnumerable<IncomeCategory> GetAll();
    /// <summary>
    /// Create record Income Category in table
    /// </summary>
    /// <param name="expenseCategory"></param>
    void Create(CreateIncomeCategoryDTO incomeCategory);
    /// <summary>
    /// Update record Income Category in table
    /// </summary>
    /// <param name="id"></param>
    /// <param name="expenseCategory"></param>
    void Update(int id ,CreateIncomeCategoryDTO incomeCategory);
    /// <summary>
    /// Remove record Income Category from table
    /// </summary>
    /// <param name="id"></param>
    void Remove(int id);
}

public class IncomeCategoryService : IIncomeCategoryService
{
    private readonly AppDbContext _context;

    public IncomeCategoryService(AppDbContext context)
    {
        _context = context;
    }
    
    public IncomeCategory GetById(int id)
    {
        var category = _context.IncomeCategories.FirstOrDefault(x => x.Id == id);
        if (category == null) throw new NotFoundException("Not found!");
        return category;
    }
    
    public IEnumerable<IncomeCategory> GetAll()
    {
        var categories = _context.IncomeCategories.ToList();
        return categories;
    }
    
    public void Create(CreateIncomeCategoryDTO incomeCategory)
    {
        var newCategory = new IncomeCategory()
        {
            Name = incomeCategory.Name,
        };

        _context.IncomeCategories.Add(newCategory);
        _context.SaveChanges();
    }
        
    public void Update(int id ,CreateIncomeCategoryDTO incomeCategory)
    {
        var dbCategory = _context.IncomeCategories.FirstOrDefault(c => c.Id == id);
        if (dbCategory == null) throw new NotFoundException("Not found!");
        dbCategory.Name = incomeCategory.Name;
        _context.SaveChanges();
    }
    
    public void Remove(int id)
    {
        var category = _context.IncomeCategories.FirstOrDefault(c => c.Id == id);
        if (category == null) throw new NotFoundException("Not found!");
        _context.IncomeCategories.Remove(category);
        _context.SaveChanges();
    }
}