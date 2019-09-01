using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BetaGamer.Models
{
    public class BetaGamerContext : DbContext
    {
        public DbSet<Juego> Juegos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public BetaGamerContext() : base("con") { }
    }
}