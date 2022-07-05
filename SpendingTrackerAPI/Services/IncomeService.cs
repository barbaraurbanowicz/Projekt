using Microsoft.EntityFrameworkCore;
using SpendingTrackerAPI.Database;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Exceptions;

namespace SpendingTrackerAPI.Services;

public interface IIncomeService
{
    IEnumerable<Income> GetAll();
    Income GetById(int id);
    void Create(Income expense);
    void Remove(int id);
    void Update(int id, Income income);
}

public class IncomeService : IIncomeService
{
    private readonly AppDbContext _context;

    public IncomeService(AppDbContext context)
    {
        _context = context;
    }
    
    
    public IEnumerable<Income> GetAll()
    {
        IQueryable<Income> incomeQuery = _context.Incomes.Include(x => x.IncomeCategory);

        var incomes = incomeQuery.ToList();
        return incomes;
    }
    
    public Income GetById(int id)
    {
        var income = _context.Incomes.Include(x=>x.IncomeCategory).FirstOrDefault(x => x.Id == id);
        if (income == null) throw new NotFoundException("Not found!");
        return income;
    }
    
    public void Create(Income expense)
    {
        var newIncome = new Income()
        {
            Name = expense.Name,
            Amount = expense.Amount,
            IncomeCategoryId = expense.IncomeCategoryId
        };
        _context.Incomes.Add(newIncome);
        _context.SaveChanges();
    }
    
    public void Remove(int id)
    {
        var income = _context.Incomes.FirstOrDefault(x => x.Id == id);
        if (income == null) throw new NotFoundException("Not found!");
        _context.Incomes.Remove(income);
        _context.SaveChanges();
    }
    
    public void Update(int id, Income income)
    {
        var incomeDB = _context.Incomes.FirstOrDefault(x => x.Id == id);
        if (incomeDB == null) throw new NotFoundException("Not found!");
        incomeDB.Name = income.Name;
        incomeDB.Amount = income.Amount;
        incomeDB.IncomeCategoryId = income.IncomeCategoryId;
        _context.SaveChanges();
    }
}