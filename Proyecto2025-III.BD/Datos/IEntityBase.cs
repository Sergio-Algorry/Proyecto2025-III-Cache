using Proyecto2025_III.Shared.ENUM;

namespace Proyecto2025_III.BD.Datos
{
    public interface IEntityBase
    {
        EnumEstadoRegistro EstadoRegistro { get; set; }
        int Id { get; set; }
        string Observacion { get; set; }
    }
}