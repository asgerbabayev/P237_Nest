using System.ComponentModel.DataAnnotations;

namespace P237_Nest.ViewModels;

public class LoginVm
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}
