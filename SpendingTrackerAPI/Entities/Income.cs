using System.ComponentModel.DataAnnotations;

namespace SpendingTrackerAPI.Entities;

public class Income
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public int IncomeCategoryId { get; set; }
    public IncomeCategory IncomeCategory { get; set; }
}