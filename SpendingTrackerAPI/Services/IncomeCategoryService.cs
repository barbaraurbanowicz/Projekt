using Microsoft.EntityFrameworkCore;
using SpendingTrackerAPI.Database;
using SpendingTrackerAPI.Entities;

namespace SpendingTrackerAPI.Services;

public interface IIncomeCategoryService
{
    IncomeCategory GetById(int id);
    IEnumerable<IncomeCategory> GetAll();
    void Create(IncomeCategory incomeCategory);
    void Update(int id ,IncomeCategory incomeCategory);
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
        return category;
    }
    
    public IEnumerable<IncomeCategory> GetAll()
    {
        var categories = _context.IncomeCategories.ToList();
        return categories;
    }
    
    public void Create(IncomeCategory incomeCategory)
    {
        var newCategory = new IncomeCategory()
        {
            Name = incomeCategory.Name,
        };

        _context.IncomeCategories.Add(newCategory);
        _context.SaveChanges();
    }
        
    public void Update(int id ,IncomeCategory incomeCategory)
    {
        var dbCategory = _context.IncomeCategories.FirstOrDefault(c => c.Id == id);
        dbCategory.Name = incomeCategory.Name;
        _context.SaveChanges();
    }
    
    public void Remove(int id)
    {
        var category = _context.IncomeCategories.FirstOrDefault(c => c.Id == id);
        _context.IncomeCategories.Remove(category);
        _context.SaveChanges();
    }
}