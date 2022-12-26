using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;   // [!] подрубил
using Microsoft.AspNetCore.Authentication;  // [!] подрубил
using Microsoft.AspNetCore.Authentication.Cookies;  // [!] подрубил
using CustomAuthAspNet6.Models;    // [!] подрубил

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomAuthAspNet6
{

    public class AccessController : Controller
    {
        // GET: /<controller>/
        public IActionResult Login()
        {
            // проверяем, действительно ли пользователь залогинился
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {
            if (modelLogin.Email == "user@example.com" &&  // находим юзера и его пароль
                modelLogin.Password == "123")
            {

                List<Claim> claims = new List<Claim>()  // закинули Email в лист. Узнать про Claim!
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties", "Example Role")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,  // передали сюда claims
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.isKeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties); // передали сюда наш claimsIdentity и properties

                return RedirectToAction("Index", "Home");

            }

            ViewData["ValidateMessage"] = "User not found!";
            return View();
        }
    }


}

