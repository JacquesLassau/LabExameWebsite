using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LabExameWebsite.Models;
using LabExameWebsite.Models.ViewModel;
using X.PagedList;

namespace LabExameWebsite.Controllers
{
    public class ExameController : Controller
    {
        private Context db = new Context();

        public ActionResult Index(int pagina = 1)
        {
            var exames = db.Exames.ToList().ToPagedList(pagina, 5);
            return View(exames);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Exame exame = db.Exames.Find(id);

            if (exame == null)
                return HttpNotFound();

            ExameViewModel tipoExameViewModel = new ExameViewModel();
            var tipoExames = db.TiposExames.ToList();

            foreach (var item in tipoExames)
            {
                tipoExameViewModel.ListarTiposExamesViewModel.Add(new SelectListItem()
                {
                    Text = Convert.ToString(item.TipoExameID) + " - " + item.NomeTipoExame,
                    Value = Convert.ToString(item.TipoExameID),
                });
            }

            tipoExameViewModel.ExameID = exame.ExameID;
            tipoExameViewModel.TipoExameID = exame.TipoExameID;
            tipoExameViewModel.NomeExame = exame.NomeExame;
            tipoExameViewModel.ObservacaoExame = exame.ObservacaoExame;

            return View(tipoExameViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ExameViewModel tipoExameViewModel = new ExameViewModel();
            var tipoExames = db.TiposExames.ToList();

            foreach (var item in tipoExames)
            {
                tipoExameViewModel.ListarTiposExamesViewModel.Add(new SelectListItem()
                {
                    Text = Convert.ToString(item.TipoExameID) + " - " + item.NomeTipoExame,
                    Value = Convert.ToString(item.TipoExameID),
                    Selected = true
                });
            }

            return View(tipoExameViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Exame exame)
        {
            if (ModelState.IsValid)
            {
                db.Exames.Add(exame);
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }

            return View(exame);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Exame exame = db.Exames.Find(id);

            if (exame == null)
                return HttpNotFound();

            ExameViewModel tipoExameViewModel = new ExameViewModel();
            var tipoExames = db.TiposExames.ToList();

            foreach (var item in tipoExames)
            {
                tipoExameViewModel.ListarTiposExamesViewModel.Add(new SelectListItem()
                {
                    Text = Convert.ToString(item.TipoExameID) + " - " + item.NomeTipoExame,
                    Value = Convert.ToString(item.TipoExameID),                    
                });
            }

            tipoExameViewModel.ExameID = exame.ExameID;
            tipoExameViewModel.TipoExameID = exame.TipoExameID;
            tipoExameViewModel.NomeExame = exame.NomeExame;
            tipoExameViewModel.ObservacaoExame = exame.ObservacaoExame;

            return View(tipoExameViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Exame exame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exame).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            return View(exame);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Exame exame = db.Exames.Find(id);

            if (exame == null)
                return HttpNotFound();

            ExameViewModel tipoExameViewModel = new ExameViewModel();
            var tipoExames = db.TiposExames.ToList();

            foreach (var item in tipoExames)
            {
                tipoExameViewModel.ListarTiposExamesViewModel.Add(new SelectListItem()
                {
                    Text = Convert.ToString(item.TipoExameID) + " - " + item.NomeTipoExame,
                    Value = Convert.ToString(item.TipoExameID),
                });
            }

            tipoExameViewModel.ExameID = exame.ExameID;
            tipoExameViewModel.TipoExameID = exame.TipoExameID;
            tipoExameViewModel.NomeExame = exame.NomeExame;
            tipoExameViewModel.ObservacaoExame = exame.ObservacaoExame;

            return View(tipoExameViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exame exame = db.Exames.Find(id);
            db.Exames.Remove(exame);
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
