namespace Alfasoft
{
    public class LoginViewModel
    {
        public string? Login { get; set; }
        public string? Password { get; set; }

        public LoginViewModel()
        {
            Login = string.Empty;
            Password = string.Empty;
        }
    }
}