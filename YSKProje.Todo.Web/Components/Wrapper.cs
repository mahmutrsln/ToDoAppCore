using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.Todo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.Todo.Web.Components
{
    public class Wrapper : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;
        public Wrapper(UserManager<AppUser> userManager, IBildirimService bildirimService, IMapper mapper)
        {
            _mapper = mapper;
            _bildirimService = bildirimService;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var identityUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var model = _mapper.Map<AppUserListDto>(identityUser);

            var bildirimler = _bildirimService.GetirOkunmayanlar(model.Id).Count;
            ViewBag.BildirimSayisi = bildirimler;

            var role = _userManager.GetRolesAsync(identityUser).Result;
            if (role.Contains("Admin"))
            {
                return View(model);
            }
            return View("Member", model);

        }
    }
}
