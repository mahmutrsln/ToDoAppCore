using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.Todo.Business.Interfaces;
using YSKProje.Todo.Web.BaseControllers;
using YSKProje.Todo.Web.StringInfo;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.Todo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class HomeController : BaseIdentityController
    {
        private readonly IGorevService _gorevService;
        private readonly IBildirimService _bildirimService;
        private readonly IRaporService _raporService;

        public HomeController(IGorevService gorevService, IBildirimService bildirimService, UserManager<AppUser> userManager, IRaporService raporService)
            : base(userManager)
        {
            _gorevService = gorevService;
            _bildirimService = bildirimService;
            _raporService = raporService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempDataInfo.Anasayfa;
            var user = await GetirGirisYapanKullanici();
            ViewBag.AtanmayiBekleyenGorevSayisi = _gorevService.GetirAtanmayiBekleyenGorevSayisiile();
            ViewBag.TamamlanmisGorevSayisi = _gorevService.GetirGorevTamamlanmis();
            ViewBag.OkunmamisBildirimSayisi = _bildirimService.GetirOkunmayanBildirimSayisiileAppUserId(user.Id);
            ViewBag.ToplamRaporSayisi = _raporService.GetirRaporSayisi();
            return View();
        }
    }
}
