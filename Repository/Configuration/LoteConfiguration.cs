using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Configuration
{
    public class LoteConfiguration : IEntityTypeConfiguration<Lote>
    {

        public void Configure(EntityTypeBuilder<Lote> builder)
        {
            builder.HasOne(x => x.Evento)
                .WithMany(x=>x.Lotes)
                .HasForeignKey(x => x.FkEvento);         
        }
    }
}
