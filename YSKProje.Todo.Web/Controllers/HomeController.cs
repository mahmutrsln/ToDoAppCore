using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.Todo.Business.Interfaces;
using YSKProje.Todo.Web.BaseControllers;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.Todo.Web.Controllers
{
    public class HomeController : BaseIdentityController
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ICustomLogger _customLogger;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ICustomLogger customLogger) : base(userManager)
        {
            _signInManager = signInManager;
            _customLogger = customLogger;
        }
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GirisYap(AppUserSignInDto model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        var roller = await _userManager.GetRolesAsync(user);
                        if (roller.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new { area = "member" });
                        }
                    }
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            }
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> KayitOl(AppUserAddDto appUserAddViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = appUserAddViewModel.UserName,
                    Email = appUserAddViewModel.Email,
                    SurName = appUserAddViewModel.SurName,
                    Name = appUserAddViewModel.Name

                };

                var result = await _userManager.CreateAsync(user, appUserAddViewModel.Password);

                if (result.Succeeded)
                {
                    var addroleResult = await _userManager.AddToRoleAsync(user, "Member");
                    if (addroleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        HataEkle(addroleResult.Errors);
                    }
                }
                HataEkle(result.Errors);
            }
            return View(appUserAddViewModel);
        }


        public async Task<IActionResult> CikisYap()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult StatusCode(int? code)
        {
            if (code == 404)
            {
                ViewBag.code = code;
                ViewBag.message = "Sayfa bulunamadı.";
            }

            return View();

        }
        public IActionResult Error()
        {
            var exceptionHanler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            _customLogger.LogError($"hatanın oluştuğu yer : {exceptionHanler.Path}" +
                $"\nHatanın Mesajı : {exceptionHanler.Error.Message}\nStack Trace : {exceptionHanler.Error.StackTrace} ");


            ViewBag.path = exceptionHanler.Path;
            ViewBag.error = exceptionHanler.Error.Message;

            return View();
        }

        public void Hata()
        {
            throw new Exception("bu bir hata");
        }

    }
}
