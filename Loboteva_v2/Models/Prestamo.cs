using System;
using System.Collections.Generic;

namespace Loboteva_v2.Models
{
    public partial class Prestamo
    {
        public Prestamo()
        {
            Sanciones = new HashSet<Sancione>();
        }

        public int Id { get; set; }
        public DateTime? FechaDePrestamo { get; set; }
        public DateTime? FechaDeDevolucion { get; set; }
        public int? IdAdministrador { get; set; }
        public string? Estado { get; set; }
        public int? IdLibro { get; set; }

        public virtual Administrador? IdAdministradorNavigation { get; set; }
        public virtual Libro? IdLibroNavigation { get; set; }
        public virtual ICollection<Sancione> Sanciones { get; set; }
    }
}
