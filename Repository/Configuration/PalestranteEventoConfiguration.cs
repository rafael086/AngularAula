using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Configuration
{
    class PalestranteEventoConfiguration : IEntityTypeConfiguration<PalestranteEvento>
    {
        public void Configure(EntityTypeBuilder<PalestranteEvento> builder)
        {
            builder.HasKey(x => new { x.FkEvento, x.FkPalestrante});

            builder.HasOne(x => x.Palestrante)
                .WithMany(x => x.PalestranteEventos)
                .HasForeignKey(x => x.FkPalestrante);
            builder.HasOne(x => x.Evento)
                .WithMany(x => x.PalestranteEventos)
                .HasForeignKey(x => x.FkEvento);
        }
    }
}
