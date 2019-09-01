using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetaGamer.Models
{
    public class Juego
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Descripción { get; set; }
        public virtual List<Comentario> Comentarios { get; set; }
    }
}