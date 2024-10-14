using System;

namespace Loboteca_v2.Models
{
    public class ELibro
    {
        public int Id { get; set; } // Identificador único del libro
        public string Titulo { get; set; } // Título del libro
        public string ImagenUrl { get; set; } // URL de la imagen de la portada del libro
        public bool Disponible { get; set; } // Indica si el libro está disponible
        public DateTime FechaDePublicacion { get; set; } // Fecha de publicación del libro
        public int IdCarrera { get; set; } // Identificador de la carrera a la que pertenece el libro
    }

    public class Revista
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string ImagenUrl { get; set; }
        public bool Disponible { get; set; }
    }
}
