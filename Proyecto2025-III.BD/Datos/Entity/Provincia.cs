using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025_III.BD.Datos.Entity
{
    /// <summary>
    /// Represents a province, including its associated country, type, and identifying information.
    /// </summary>
    /// <remarks>This class is used to model a province entity, which includes details such as its associated
    /// country, type of province, a unique IGM code, and the province's name. The <see cref="IGM_Id"/> property is
    /// indexed and must be unique.</remarks>
    [Index(nameof(IGM_Id), Name = "Provincia_IGM_Id_UQ", IsUnique = true)]
    public class Provincia : EntityBase
    {
        /// <summary>
        /// Gets or sets the identifier of the country.
        /// </summary>
        [Required(ErrorMessage = "El País es obligatorio.")]
        public required int PaisId { get; set; }
        public Pais? Pais { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the type of province.
        /// </summary>
        [Required(ErrorMessage = "El tipo de provincia es obligatorio.")]
        public required int TipoProvinciaId { get; set; }
        public TipoProvincia? TipoProvincia { get; set; }

        [Required(ErrorMessage = "El Código IGM es obligatorio.")]
        [MaxLength(2, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public required string IGM_Id { get; set; }

        [Required(ErrorMessage = "El Nombre de la Provincia es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public required string NombreProvincia { get; set; }
    }
}
