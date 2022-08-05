/*
 * 
 * Analista: Jacques de Lassau
 * Data: 04/08/2022 23:15h
 * Modificações: Modificadas variáveis do tipo "var" para seus tipos originais; 
 * centralizado na função "CarregarTiposDeExame" valores dos combos para tipo de exame
 * 
 */

using System;
using System.Collections.Generic;
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
            return View(db.Exames.ToList().ToPagedList(pagina, 5));
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ExameViewModel tipoExameViewModel = CarregarTiposDeExame(false, false, id.Value);

            if (tipoExameViewModel == null)
                return HttpNotFound();

            return View(tipoExameViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ExameViewModel tipoExameViewModel = CarregarTiposDeExame(true, true);

            if (tipoExameViewModel == null)
                return HttpNotFound();

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

            ExameViewModel tipoExameViewModel = CarregarTiposDeExame(false, false, id.Value);

            if (tipoExameViewModel == null)
                return HttpNotFound();

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

            ExameViewModel tipoExameViewModel = CarregarTiposDeExame(false, false, id.Value);

            if (tipoExameViewModel == null)
                return HttpNotFound();

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

        private ExameViewModel CarregarTiposDeExame(bool pPaginaCreate, bool pOpcaoSelecionada, int? pId = null)
        {
            Exame exame = db.Exames.Find(pId);
            if (exame != null || pPaginaCreate)
            {
                ExameViewModel tipoExameViewModel = new ExameViewModel();
                List<TipoExame> tipoExames = db.TiposExames.ToList();

                foreach (TipoExame item in tipoExames)
                {
                    tipoExameViewModel.ListarTiposExamesViewModel.Add(new SelectListItem()
                    {
                        Text = string.Concat(Convert.ToString(item.TipoExameID), " - ", item.NomeTipoExame),
                        Value = Convert.ToString(item.TipoExameID),
                        Selected = pOpcaoSelecionada
                    });
                }

                if (!pPaginaCreate)
                {
                    tipoExameViewModel.ExameID = exame.ExameID;
                    tipoExameViewModel.TipoExameID = exame.TipoExameID;
                    tipoExameViewModel.NomeExame = exame.NomeExame;
                    tipoExameViewModel.ObservacaoExame = exame.ObservacaoExame;
                }

                return tipoExameViewModel;
            }
            else
                return null;

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
