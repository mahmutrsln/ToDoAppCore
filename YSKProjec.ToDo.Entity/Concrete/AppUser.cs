using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.Entities.Concrete
{
    public class AppUser : IdentityUser<int>, ITablo
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Picture { get; set; } = "default.png";
        public List<Gorev> Gorevler { get; set; }
        public List<Bildirim> Bildirims { get; set; }

    }
}
