namespace SpendingTrackerAPI.DTOModels;

public class CreateIncomeDTO
{
    public string Name { get; set; }
    public int Amount { get; set; }
    public int IncomeCategoryId { get; set; }
}