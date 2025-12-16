using EspacoTech.Areas.Identity.Data;
using EspacoTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EspacoTech.Areas.Identity.Data;

public class EspacoTechLoginContext : IdentityDbContext<Usuario>
{
    public EspacoTechLoginContext(DbContextOptions<EspacoTechLoginContext> options)
        : base(options)
    {
    }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configuração da Reserva
        builder.Entity<Reserva>()
                .HasOne(r => r.UsuarioPerfil)
                .WithMany(u => u.Reservas)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

        // Configuração da Sala
        builder.Entity<Reserva>()
            .HasOne(r => r.Sala)
            .WithMany(s => s.Reservas)
            .HasForeignKey(r => r.IdSala)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public class UserConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(x => x.NomeUsuario).HasMaxLength(100).IsRequired();
        }
    }

}
