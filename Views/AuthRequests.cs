namespace DSLManagement.Views;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginResult
{
    public bool Succeeded { get; set; }
    public string Token { get; set; }
    public List<string> Errors { get; set; }
}

public class RegisterResult
{
    public bool Succeeded { get; set; }
    public List<string> Errors { get; set; }
}