using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts
{
    public class TodoContext : IdentityDbContext<AppUser, AppRole, int>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //buraya kendidatabase'imizin connection stringi eklenmeli ve mutlaka migration yapılmalı.
            // optionsBuilder.UseSqlServer("server=DESKTOP-ULMF4IC;database=ToDo;user id =sa;password=654321;");
            optionsBuilder.UseSqlServer("server=DESKTOP-ULMF4IC;database=ToDo;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  modelBuilder.ApplyConfiguration(new KullaniciMap());
            modelBuilder.ApplyConfiguration(new AppUserMapping());
            modelBuilder.ApplyConfiguration(new GorevMapping());
            modelBuilder.ApplyConfiguration(new AciliyetMap());
            modelBuilder.ApplyConfiguration(new RaporMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Gorev> Gorevler { get; set; }

        public DbSet<Rapor> Rapors { get; set; }
        public DbSet<Aciliyet> Aciliyets { get; set; }
        public DbSet<Bildirim> Bildirims { get; set; }





    }
}
