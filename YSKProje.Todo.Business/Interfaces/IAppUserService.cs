using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.Todo.Business.Interfaces
{
    public interface IAppUserService
    {
        List<AppUser> getirAdminOlmayanlar();

        List<AppUser> getirAdminOlmayanlar(out int toplamSayfa, string search, int aktifSayfa = 1);

        List<DualHelper> GetirEnCokGorevTamamlamisPersoneller();

        List<DualHelper> GetirEnCokGorevdeCalisanPersoneller();
    }
}
