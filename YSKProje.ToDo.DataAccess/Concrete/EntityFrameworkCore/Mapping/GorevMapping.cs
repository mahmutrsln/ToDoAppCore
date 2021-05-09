using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class GorevMapping : IEntityTypeConfiguration<Gorev>
    {
        public void Configure(EntityTypeBuilder<Gorev> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Ad).HasMaxLength(100);
            builder.Property(x => x.Aciklama).HasColumnType("ntext");

            builder.HasOne(I => I.Aciliyet).WithMany(I => I.Gorevs)
                .HasForeignKey(I => I.AciliyetId);

            // builder.HasOne(x => x.Kullanici).WithMany(x => x.Calismalar).HasForeignKey(x => x.KullaniciId);

        }
    }
}
