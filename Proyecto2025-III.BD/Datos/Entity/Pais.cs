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
    /// Represents a country with its associated international and local codes, as well as its name.
    /// </summary>
    /// <remarks>This class provides properties to store information about a country, including its ISO 3166-1
    /// alpha-3 code, INDEC code, calling code, and name. The <see cref="Alpha3Code"/> and <see cref="CodIndec"/>
    /// properties are unique and required, ensuring that each country is uniquely identifiable.</remarks>
    [Index(nameof(Alpha3Code), Name = "Pais_Alpha3Code_UQ", IsUnique = true)]
    [Index(nameof(CodIndec), Name = "Pais_CodIndec_UQ", IsUnique = true)]
    public class Pais : EntityBase
    {
        /// <summary>
        /// Gets or sets the three-letter international country code (ISO 3166-1 alpha-3 format).
        /// </summary>
        [Required(ErrorMessage = "El codigo de país internacional es obligatorio")]
        [MaxLength(3,ErrorMessage ="Maxima cant. caracteres 3")]
        public required string Alpha3Code { get; set; }

        [Required(ErrorMessage = "El Código de País de INDEC de 3 caracteres Numericos  es obligatorio.")]
        [MaxLength(3, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public required string CodIndec { get; set; }

        public string CodLlamada { get; set; } = "";

        [Required(ErrorMessage = "El Nombre del País es obligatorio.")]
        [MaxLength(200, ErrorMessage = "El campo Código del País tiene como máximo {1} caracteres.")]
        public required string NombrePais { get; set; }
    }
}
