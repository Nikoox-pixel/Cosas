using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetaGamer.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
        public virtual Juego Juego { get; set; }
    }
}