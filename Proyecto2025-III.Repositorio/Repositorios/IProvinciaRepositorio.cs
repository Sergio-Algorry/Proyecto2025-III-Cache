using Proyecto2025_III.BD.Datos.Entity;
using Proyecto2025_III.Shared.DTO;

namespace Proyecto2025_III.Repositorio.Repositorios
{
    public interface IProvinciaRepositorio : IRepositorio<Provincia>
    {
        Task<ProvinciaResumenDTO?> ResumenProvincia(string cod);
        Task<Provincia?> SelectByIGM_Id(string cod);
    }
}