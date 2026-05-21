using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto2025_III.BD.Datos.Entity;
using Proyecto2025_III.Shared.ENUM;
using Proyecto2025_III.BD.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025_III.BD.Datos
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Pais>  Paises { get; set; }
        public DbSet<TipoProvincia> TipoProvincias { get; set; }
        public DbSet<Provincia> Provincias { get; set; }


        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aquí puedes configurar tus entidades y relaciones

            var cascadeFKs = modelBuilder.Model
                .G­etEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict;
            }
        }
    }
}
