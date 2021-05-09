using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfRaporRespository : EfGenericRepository<Rapor>, IRaporDal
    {
        public Rapor GetirGorevileId(int id)
        {
            using var context = new TodoContext();
            return context.Rapors.Include(I => I.Gorev).ThenInclude(I => I.Aciliyet).Where(I => I.Id == id).FirstOrDefault();
        }

        public int GetirRaporSayisi()
        {
            using var context = new TodoContext();
            return context.Rapors.Count();
        }

        public int GetirRaporSayisiileAppUserId(int id)
        {
            using var context = new TodoContext();
            var result = context.Gorevler.Include(I => I.Rapors).Where(I => I.AppUserId == id);
            return result.SelectMany(I => I.Rapors).Count();
        }
    }
}
