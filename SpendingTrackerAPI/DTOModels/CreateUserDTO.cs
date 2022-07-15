namespace SpendingTrackerAPI.DTOModels;

public class CreateUserDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; } = 1;
}