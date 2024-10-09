using System;
using System.Collections.Generic;

namespace Loboteva_v2.Models
{
    public partial class Autor
    {
        public Autor()
        {
            AutorELibros = new HashSet<AutorELibro>();
            AutorLibros = new HashSet<AutorLibro>();
            AutorRevista = new HashSet<AutorRevistum>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }

        public virtual ICollection<AutorELibro> AutorELibros { get; set; }
        public virtual ICollection<AutorLibro> AutorLibros { get; set; }
        public virtual ICollection<AutorRevistum> AutorRevista { get; set; }
    }
}
