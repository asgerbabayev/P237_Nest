using Microsoft.AspNetCore.Identity;
using P237_Nest.Models;

namespace P237_Nest.Services;
public class LayoutService : ILayoutService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
    public LayoutService(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<UserGetDto> GetUser()
    {
        string username = _contextAccessor!.HttpContext!.User!.Identity!.Name!;
        var user = await _userManager.FindByNameAsync(username);
        if (user == null) throw new Exception("User not found");
        return new UserGetDto
        {
            FullName = $"{user.Name} {user.Surname}"
        };
    }
}












public class UserGetDto
{
    public string FullName { get; set; }
};
