using Proyecto2025_III.BD.Datos.Entity;

namespace Proyecto2025_III.Repositorio.Repositorios
{
    public interface ITipoProvinciaRepositorio : IRepositorio<TipoProvincia>
    {
        Task<TipoProvincia?> SelectByCod(string cod);
    }
}