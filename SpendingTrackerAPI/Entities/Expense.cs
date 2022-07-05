using System.ComponentModel.DataAnnotations;

namespace SpendingTrackerAPI.Entities;

public class Expense
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public int ExpenseCategoryId { get; set; }
    public ExpenseCategory ExpenseCategory { get; set; }
}