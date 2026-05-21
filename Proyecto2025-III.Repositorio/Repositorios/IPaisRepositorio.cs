using Proyecto2025_III.BD.Datos.Entity;
using Proyecto2025_III.Shared.DTO;

namespace Proyecto2025_III.Repositorio.Repositorios
{
    public interface IPaisRepositorio : IRepositorio<Pais>
    {
        Task<Pais?> SelectByAlpha3Code(string cod);
        Task<Pais?> SelectByCodIndec(string cod);
        Task<List<PaisListadoDTO>> SelectListaPais();
    }
}