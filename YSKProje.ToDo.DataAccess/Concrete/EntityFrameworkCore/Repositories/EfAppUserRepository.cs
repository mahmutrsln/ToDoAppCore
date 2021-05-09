using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAppUserRepository : IAppUserDal
    {
        public List<AppUser> getirAdminOlmayanlar()
        {
            using var context = new TodoContext();
            return context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {
                user = resultUser,
                userRole = resultUserRole
            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id, (resultTable, resultRole) => new
            {
                user = resultTable.user,
                userRoles = resultTable.userRole,
                roles = resultRole
            }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()
            {
                Id = I.user.Id,
                Name = I.user.Name,
                SurName = I.user.SurName,
                Picture = I.user.Picture,
                Email = I.user.Email,
                UserName = I.user.UserName
            }).ToList();
        }

        public List<AppUser> getirAdminOlmayanlar(out int toplamSayfa, string search, int aktifSayfa = 1)
        {
            using var context = new TodoContext();
            var result = context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {
                user = resultUser,
                userRole = resultUserRole
            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id, (resultTable, resultRole) => new
            {
                user = resultTable.user,
                userRoles = resultTable.userRole,
                roles = resultRole
            }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()
            {
                Id = I.user.Id,
                Name = I.user.Name,
                SurName = I.user.SurName,
                Picture = I.user.Picture,
                Email = I.user.Email,
                UserName = I.user.UserName
            });

            toplamSayfa = (int)Math.Ceiling((double)result.Count() / 3);

            if (!string.IsNullOrWhiteSpace(search))
            {
                result = result.Where(I => I.Name.ToLower().Contains(search.ToLower()) || I.SurName.ToLower().Contains(search.ToLower()));
                toplamSayfa = (int)Math.Ceiling((double)result.Count() / 3);
            }

            return result.Skip((aktifSayfa - 1) * 3).Take(3).ToList();
        }

        public List<DualHelper> GetirEnCokGorevTamamlamisPersoneller()
        {
            using var context = new TodoContext();
            return context.Gorevler
                  .Include(I => I.AppUser)
                  .Where(I => I.Durum)
                  .GroupBy(I => I.AppUser.Name)
                  .OrderByDescending(I => I.Count()).Take(5)
                  .Select(I => new DualHelper
                  {
                      GorevSayisi = I.Count(),
                      Isim = I.Key
                  }).ToList();


        }

        public List<DualHelper> GetirEnCokGorevdeCalisanPersoneller()
        {
            using var context = new TodoContext();
            return context.Gorevler
                .Include(I => I.AppUser)
                .Where(I => !I.Durum && I.AppUserId != null)
                .GroupBy(I => I.AppUser.Name)
                .OrderByDescending(I => I.Count()).Take(5)
                .Select(I => new DualHelper
                {
                    GorevSayisi = I.Count(),
                    Isim = I.Key
                }).ToList();


        }
    }
}
