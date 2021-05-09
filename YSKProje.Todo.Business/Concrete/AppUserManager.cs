using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.Todo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.Todo.Business.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserDal _appUserDal;
        public AppUserManager(IAppUserDal appUserDal)
        {
            _appUserDal = appUserDal;
        }
        public List<AppUser> getirAdminOlmayanlar()
        {
            return _appUserDal.getirAdminOlmayanlar();
        }

        public List<AppUser> getirAdminOlmayanlar(out int toplamSayfa, string search, int aktifSayfa = 1)
        {
            return _appUserDal.getirAdminOlmayanlar(out toplamSayfa, search, aktifSayfa);
        }

        public List<DualHelper> GetirEnCokGorevdeCalisanPersoneller()
        {
            return _appUserDal.GetirEnCokGorevdeCalisanPersoneller();
        }

        public List<DualHelper> GetirEnCokGorevTamamlamisPersoneller()
        {
            return _appUserDal.GetirEnCokGorevTamamlamisPersoneller();
        }
    }
}
