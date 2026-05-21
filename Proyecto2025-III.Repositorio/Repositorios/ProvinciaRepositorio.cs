using Microsoft.EntityFrameworkCore;
using Proyecto2025_III.BD.Datos;
using Proyecto2025_III.BD.Datos.Entity;
using Proyecto2025_III.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025_III.Repositorio.Repositorios
{
    public class ProvinciaRepositorio : Repositorio<Provincia>,
                                        IProvinciaRepositorio
    {
        private readonly AppDbContext context;

        public ProvinciaRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Provincia?> SelectByIGM_Id(string cod)
        {
            Provincia? entidad = await context.Provincias
                .Include(p => p.Pais)
                .Include(p => p.TipoProvincia)
                .FirstOrDefaultAsync(x => x.IGM_Id == cod);

            return entidad;
        }

        public async Task<ProvinciaResumenDTO?> ResumenProvincia(string cod)
        {
            var entidad = await context.Provincias
                                .Where(x => x.IGM_Id == cod)
                                .Select(p => new ProvinciaResumenDTO
                                {
                                    Id = p.Id,
                                    NombrePais = p.Pais!.NombrePais,
                                    CodProvincia = p.TipoProvincia!.Codigo,
                                    IGM_Id = p.IGM_Id,
                                    NombreProvincia = p.NombreProvincia
                                })
                                .FirstOrDefaultAsync();
            return entidad;
        }
    }
}
