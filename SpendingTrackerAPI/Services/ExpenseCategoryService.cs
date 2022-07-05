﻿using SpendingTrackerAPI.Database;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Exceptions;

namespace SpendingTrackerAPI.Services;

public interface IExpenseCategoryService
{
    ExpenseCategory GetById(int id);
    IEnumerable<ExpenseCategory> GetAll();
    void Create(ExpenseCategory expenseCategory);
    void Update(int id ,ExpenseCategory expenseCategory);
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
    
    public void Create(ExpenseCategory expenseCategory)
    {
        var newCategory = new ExpenseCategory()
        {
            Name = expenseCategory.Name,
        };

        _context.ExpenseCategories.Add(newCategory);
        _context.SaveChanges();
    }
        
    public void Update(int id ,ExpenseCategory expenseCategory)
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