using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using System;

namespace Repository
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LoteConfiguration());
            modelBuilder.ApplyConfiguration(new RedeSocialConfiguration());
            modelBuilder.ApplyConfiguration(new PalestranteEventoConfiguration());
            modelBuilder.Entity<Evento>().HasData(
                new Evento{Id = 1, DataEvento = Convert.ToDateTime("01/01/2020"),Email = "email1@email.com",ImagemURL = "img1.jpg",Local = "SP",Lote = "001",QtdPessoas = 100,Telefone = "09090909",Tema = ".NET Core"},
                new Evento { Id = 2, DataEvento = Convert.ToDateTime("01/02/2020"), Email = "email2@email.com", ImagemURL = "img2.jpg", Local = "RJ", Lote = "002", QtdPessoas = 200, Telefone = "09090909", Tema = "ANGULAR" },
                new Evento { Id = 3, DataEvento = Convert.ToDateTime("01/03/2020"), Email = "email3@email.com", ImagemURL = "img3.jpg", Local = "MG", Lote = "003", QtdPessoas = 300, Telefone = "09090909", Tema = ".NET Core e ANGULAR" }
                );
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }
    }
}
