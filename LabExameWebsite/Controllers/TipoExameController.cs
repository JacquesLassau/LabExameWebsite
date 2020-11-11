using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LabExameWebsite.Models;
using X.PagedList;

namespace LabExameWebsite.Controllers
{
    public class TipoExameController : Controller
    {
        private Context db = new Context();

        [HttpGet]
        public ActionResult Index(int pagina = 1)
        {
            return View(db.TiposExames.OrderBy(a => a.TipoExameID).ToPagedList(pagina, 5));
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);            

            TipoExame tipoExame = db.TiposExames.Find(id);

            if (tipoExame == null)
                return HttpNotFound();

            return View(tipoExame);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoExame tipoExame)
        {
            if (ModelState.IsValid)
            {
                db.TiposExames.Add(tipoExame);
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }

            return View(tipoExame);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            TipoExame tipoExame = db.TiposExames.Find(id);

            if (tipoExame == null)
                return HttpNotFound();

            return View(tipoExame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoExame tipoExame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoExame).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            return View(tipoExame);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            TipoExame tipoExame = db.TiposExames.Find(id);

            if (tipoExame == null)
                return HttpNotFound();

            return View(tipoExame);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoExame tipoExame = db.TiposExames.Find(id);
            db.TiposExames.Remove(tipoExame);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
