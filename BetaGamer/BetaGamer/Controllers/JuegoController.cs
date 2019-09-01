using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BetaGamer.Models;

namespace BetaGamer.Controllers
{
    public class JuegoController : Controller
    {
        private BetaGamerContext db = new BetaGamerContext();

        // GET: Juego
        public ActionResult Index()
        {
            return View(db.Juegos.ToList());
        }

        // GET: /Juego/Details/5
        public ActionResult Details(int id = 0)
        {
            //var db = new BetaGamerContext();
            Juego juego = db.Juegos.Find(id);
            if (juego == null)
            {
                return HttpNotFound();
            }
            //cargamos explicitamente sus comentarios
            db.Entry(juego).Collection(l => l.Comentarios).Load();
            return View(juego);
        }

        // GET: Juego/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Juego/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,FechaPublicacion,Descripción")] Juego juego)
        {
            if (ModelState.IsValid)
            {
                db.Juegos.Add(juego);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(juego);
        }

        // GET: Juego/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Juego juego = db.Juegos.Find(id);
            if (juego == null)
            {
                return HttpNotFound();
            }
            return View(juego);
        }

        // POST: Juego/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,FechaPublicacion,Descripción")] Juego juego)
        {
            if (ModelState.IsValid)
            {
                db.Entry(juego).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(juego);
        }

        // GET: Juego/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Juego juego = db.Juegos.Find(id);
            if (juego == null)
            {
                return HttpNotFound();
            }
            return View(juego);
        }

        // POST: Juego/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Juego juego = db.Juegos.Find(id);
            db.Juegos.Remove(juego);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult SearchIndex(string searchString) {
            var juegos = from m in db.Juegos select m;
            if (!String.IsNullOrEmpty(searchString)) {
                juegos = juegos.Where(s => s.Titulo.Contains(searchString));

                }
            return View(juegos.ToList());
        }

        public ActionResult AgregarComentario(int id)
        {
            using (var db = new BetaGamerContext())
            {
                ViewBag.Juego = db.Juegos.Find(id);
                return View();
            }
        }//Para esta accion vamos a crear una vista de detall

        [HttpPost]
        public ActionResult AgregarComentario(Comentario com) {
            try {
                //obtenemos el id del juego comentado viene en el httpRequest desde el input id="JuegoID"
                var JuegoID = int.Parse(Request["JuegoID"]);

                using (var db = new BetaGamerContext()) {
                    //Buscamos el juego y lo asignamos al comentario
                    com.Juego = db.Juegos.Find(JuegoID);
                    db.Comentarios.Add(com);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
    }
}
