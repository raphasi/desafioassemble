using Microsoft.AspNetCore.Identity;

namespace ShopTFTEC.WebApp.Context;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }  = string.Empty;
    public string LastName { get; set; } = String.Empty;   
}
