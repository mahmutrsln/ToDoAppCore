using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfBildirimRepository : EfGenericRepository<Bildirim>, IBildirimDal
    {
        public int GetirOkunmayanBildirimSayisiileAppUserId(int appUserId)
        {
            using var context = new TodoContext();
            return context.Bildirims.Count(I => !I.Durum && I.AppUserId == appUserId);
        }

        public List<Bildirim> GetirOkunmayanlar(int appUserId)
        {
            using var context = new TodoContext();
            return context.Bildirims
                .Where(I => I.Durum == false && I.AppUserId == appUserId)
                .ToList();
        }
    }
}
