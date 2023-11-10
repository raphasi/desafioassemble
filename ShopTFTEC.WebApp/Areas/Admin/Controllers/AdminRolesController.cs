using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopTFTEC.WebApp.Areas.Admin.Models;
using ShopTFTEC.WebApp.Context;
using System.ComponentModel.DataAnnotations;


namespace ShopTFTEC.WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminRolesController : Controller
{
    private RoleManager<IdentityRole> roleManager;
    private UserManager<ApplicationUser> userManager;

    public AdminRolesController(RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    public IActionResult Index()
    {
        var users = roleManager.Roles;
        return View(users);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create([Required] string name)
    {
        if (ModelState.IsValid)
        {
            IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
        }
        return View(name);
    }

    [HttpGet]
    public async Task<IActionResult> Update(string id)
    {
        IdentityRole role = await roleManager.FindByIdAsync(id);

        List<ApplicationUser> members = new List<ApplicationUser>();
        List<ApplicationUser> nonMembers = new List<ApplicationUser>();

        foreach (ApplicationUser item in userManager.Users)
        {
            if (!await userManager.IsInRoleAsync(item, role.Name))
                members.Add(item);
            else
                nonMembers.Add(item);
        }

        var model = new RoleEdit
        {
            Role = role,
            NonMembers = nonMembers,
            Members = members
        };
        return View(model);
    }

    private void Errors(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError("", error.Description);
    }

}
