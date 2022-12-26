using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CustomAuthAspNet6.Models;

using Microsoft.AspNetCore.Authentication; // [!] Подключил
using Microsoft.AspNetCore.Authentication.Cookies;  // [!] Подключил
using Microsoft.AspNetCore.Authorization; // [!] Подключил

namespace CustomAuthAspNet6.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }



    [Authorize]  // ТЕПЕРЬ СЮДА МОГУТ ЗАХОДИТЬ ТОЛЬКО ВОШЕДШИЙ ПОЛЬЗОВАТЕЛЬ
    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize]  // ТЕПЕРЬ СЮДА МОГУТ ЗАХОДИТЬ ТОЛЬКО ВОШЕДШИЙ ПОЛЬЗОВАТЕЛЬ
    public IActionResult About()
    {
        return View();
    }


    // Выход из пользователя
    public async Task<IActionResult> LogOut()
    {
        // Если вышел из аккаунта -> перенаправить на страницу входа
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Access");
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

