using System;
using System.Collections.Generic;

namespace Loboteva_v2.Models
{
    public partial class Sancione
    {
        public int IdSancion { get; set; }
        public string? Estado { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPrestamo { get; set; }
        public int? IdPenalizacion { get; set; }

        public virtual Alumno? IdAlumnoNavigation { get; set; }
        public virtual ConfiguracionPenalizacione? IdPenalizacionNavigation { get; set; }
        public virtual Prestamo? IdPrestamoNavigation { get; set; }
    }
}
