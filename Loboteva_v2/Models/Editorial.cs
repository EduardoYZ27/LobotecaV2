using System;
using System.Collections.Generic;

namespace Loboteva_v2.Models
{
    public partial class Editorial
    {
        public Editorial()
        {
            ELibros = new HashSet<ELibro>();
            Libros = new HashSet<Libro>();
            Revista = new HashSet<Revistum>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<ELibro> ELibros { get; set; }
        public virtual ICollection<Libro> Libros { get; set; }
        public virtual ICollection<Revistum> Revista { get; set; }
    }
}
