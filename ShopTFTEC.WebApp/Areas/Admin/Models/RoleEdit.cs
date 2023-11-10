using Microsoft.AspNetCore.Identity;
using ShopTFTEC.WebApp.Context;

namespace ShopTFTEC.WebApp.Areas.Admin.Models;

public class RoleEdit
{
    public IdentityRole? Role { get; set; }
    public IEnumerable<ApplicationUser>? Members { get; set; }
    public IEnumerable<ApplicationUser>? NonMembers { get; set; }
}
