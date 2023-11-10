using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopTFTEC.WebApp.Areas.Admin.Models;
using ShopTFTEC.WebApp.Context;
using ShopTFTEC.WebApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopTFTEC.WebApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;

    public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }


    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Copia os dados do RegisterViewModel para o IdentityUser
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //await _signInManager.SignInAsync(user, isPersistent: false);
                await userManager.AddToRoleAsync(user, "Cliente");
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (result.Errors.Count() == 1)
                    this.ModelState.AddModelError("Registro", "Falha ao cadastrar usuário");
                else
                {
                    foreach (var item in result.Errors)
                    {
                        var mensagem = string.Empty;

                        if (item.Description.Contains("Passwords must be at least 6 characters."))
                            mensagem = "As senhas devem ter pelo menos 6 caracteres.";
                        if (item.Description.Contains("Passwords must have at least one non alphanumeric character."))
                            mensagem = "As senhas devem ter pelo menos um caractere não alfanumérico.";
                        if (item.Description.Contains("Passwords must have at least one digit ('0'-'9')."))
                            mensagem = "As senhas devem ter pelo menos um dígito ('0'-'9').";
                        if (item.Description.Contains("Passwords must have at least one uppercase ('A'-'Z')."))
                            mensagem = "As senhas devem ter pelo menos uma letra maiúscula ('A'-'Z').";

                        this.ModelState.AddModelError("Registro", mensagem);
                    }
                }
            }
        }
        return View(model);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginAdmViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(model.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Falha ao realizar o login!!");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("index", "home");
    }

    [HttpGet]
    [Route("/Account/AccessDenied")]
    public ActionResult AccessDenied()
    {
        return View();
    }
}
