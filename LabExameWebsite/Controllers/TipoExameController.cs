/*
 * 
 * Analista: Jacques de Lassau
 * Data: 04/08/2022 23:19h
 * Modificações: Renomeados objetos de parâmetros de entrada para que sejam identificados como parâmetros dentro das actions;
 * 
 */

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
        public ActionResult Create(TipoExame pTipoExame)
        {
            if (ModelState.IsValid)
            {
                db.TiposExames.Add(pTipoExame);
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }

            return View(pTipoExame);
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
        public ActionResult Edit(TipoExame pTipoExame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pTipoExame).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            return View(pTipoExame);
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
