namespace UserServiceApi.DTOs;

public class LogInRequest{
    public string Email{get; set;} = null!;
    public string Password{get; set;} = null!;
}