namespace SpendingTrackerAPI.DTOModels;

public class CreateExpenseDTO
{
    public string Name { get; set; }
    public int Amount { get; set; }
    public int ExpenseCategoryId { get; set; }
}