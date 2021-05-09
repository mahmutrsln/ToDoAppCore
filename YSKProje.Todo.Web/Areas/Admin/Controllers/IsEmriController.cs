using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.Todo.Business.Interfaces;
using YSKProje.Todo.Web.StringInfo;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;
using YSKProje.ToDo.DTO.DTOs.RaporDtos;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.Todo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class IsEmriController : Controller
    {

        private readonly IAppUserService _appUserService;
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDosyaService _dosyaService;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;
        public IsEmriController(IAppUserService appUserService, IGorevService gorevService, UserManager<AppUser> userManager, IDosyaService dosyaService, IBildirimService bildirimService, IMapper mapper)
        {
            _mapper = mapper;
            _bildirimService = bildirimService;
            _dosyaService = dosyaService;
            _appUserService = appUserService;
            _gorevService = gorevService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            TempData["Active"] = TempDataInfo.IsEmri;
            return View(_mapper.Map<List<GorevListAllDto>>(_gorevService.GetirTumTablolarla()));
        }


        public IActionResult AtaPersonel(int id, string s, int sayfa = 1)
        {
            TempData["Active"] = TempDataInfo.IsEmri;


            ViewBag.AktifSayfa = sayfa;
            ViewBag.aranan = s;
            var personeller = _mapper.Map<List<AppUserListDto>>(_appUserService.getirAdminOlmayanlar(out int toplaSayfaaaaaa, s, sayfa));
            ViewBag.ToplamSayfa = toplaSayfaaaaaa;
            ViewBag.Personeller = personeller;
            return View(_mapper.Map<GorevListDto>(_gorevService.GetirAciliyetileId(id)));
        }

        [HttpPost]
        public IActionResult AtaPersonel(PersonelGorevlendirDto model)
        {
            Gorev ggorev = _gorevService.GetirIdile(model.gorevId);
            ggorev.AppUserId = model.userId;
            _gorevService.Guncelle(ggorev);
            _bildirimService.Kaydet(new Bildirim
            {
                AppUserId = model.userId,
                Aciklama = $"{ggorev.Ad} adlı iş için görevlendirildiniz."
            });
            return RedirectToAction("Index");
        }

        public IActionResult Detaylandir(int id)
        {
            TempData["Active"] = TempDataInfo.IsEmri;
            return View(_mapper.Map<GorevListAllDto>(_gorevService.GetirRaporlarileId(id)));
        }

        public IActionResult GetirPdf(int id)
        {
            var path = _dosyaService.AktarPdf(_mapper.Map<List<RaporDosyaDto>>(_gorevService.GetirRaporlarileId(id).Rapors));
            return File(path, "application/pdf", Guid.NewGuid() + ".pdf");

        }

        public IActionResult GetirExcel(int id)
        {
            return File(_dosyaService.AktarExcel(_mapper.Map<List<RaporDosyaDto>>(_gorevService.GetirRaporlarileId(id).Rapors)), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Guid.NewGuid() + ".xlsx");
        }



        public IActionResult GorevlendirPersonel(PersonelGorevlendirDto model)
        {
            TempData["Active"] = TempDataInfo.IsEmri;
            PersonelGorevlendirListDto personelGorevlendirmodel = new PersonelGorevlendirListDto
            {
                AppUser = _mapper.Map<AppUserListDto>(_userManager.Users.FirstOrDefault(I => I.Id == model.userId)),
                Gorev = _mapper.Map<GorevListDto>(_gorevService.GetirAciliyetileId(model.gorevId))
            };

            return View(personelGorevlendirmodel);
        }
    }
}
