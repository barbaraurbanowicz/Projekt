using System.ComponentModel.DataAnnotations;

namespace SpendingTrackerAPI.Entities;

public class ExpenseCategory
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}