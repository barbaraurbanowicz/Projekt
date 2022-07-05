using System.ComponentModel.DataAnnotations;

namespace SpendingTrackerAPI.Entities;

public class IncomeCategory
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}