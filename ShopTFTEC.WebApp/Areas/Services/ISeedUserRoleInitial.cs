namespace ShopTFTEC.WebApp.Areas.Services;

public interface ISeedUserRoleInitial
{
    Task SeedRolesAsync();
    Task SeedUsersAsync();
}
