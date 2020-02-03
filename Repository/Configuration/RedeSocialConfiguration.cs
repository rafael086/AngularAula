using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Configuration
{
    public class RedeSocialConfiguration : IEntityTypeConfiguration<RedeSocial>
    {
        public void Configure(EntityTypeBuilder<RedeSocial> builder)
        {
            builder.HasOne(x => x.Evento)
                .WithMany(x => x.RedeSociais)
                .HasForeignKey(x => x.FkEvento);

            builder.HasOne(x => x.Palestrante)
                .WithMany(x => x.RedeSociais)
                .HasForeignKey(x => x.FkPalestrante);
        }
    }
}
