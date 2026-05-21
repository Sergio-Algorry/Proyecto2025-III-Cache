using Proyecto2025_III.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025_III.BD.Datos
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Estado del registro es obligatorio.")]
        public EnumEstadoRegistro EstadoRegistro { get; set; } = EnumEstadoRegistro.EnGrabacion;
        public string Observacion { get; set; } = string.Empty;
    }
}
