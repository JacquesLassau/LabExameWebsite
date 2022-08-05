/*
 * 
 * Analista: Jacques de Lassau
 * Data: 04/08/2022 23:17h
 * Modificações: Modificados textos dispersos em variáveis constanntes;
 * Ajustes de variáveis do tipo "var" para seus tipos originais.
 * 
 */

using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
using LabExameWebsite.Models;
using X.PagedList;
using LabExameWebsite.Infrastructure;
using System;

namespace LabExameWebsite.Controllers
{
    public class PacienteController : Controller
    {
        private Context db = new Context();

        [HttpGet]
        public ActionResult Index(int pagina = 1)
        {
            return View(db.Pacientes.ToList().ToPagedList(pagina, 5));
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Paciente paciente = db.Pacientes.Find(id);

            if (paciente == null)
                return HttpNotFound();

            return View(paciente);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Paciente pPaciente)
        {
            try
            {
                if (!Uteis.CpfPacienteValido(pPaciente.CpfPaciente))
                {
                    TempData[Constantes.MensagemAlerta] = Constantes.CpfInvalido;
                }
                else
                {
                    Paciente paciente = db.Pacientes.FirstOrDefault(p => p.CpfPaciente == pPaciente.CpfPaciente);

                    if (paciente != null && !string.IsNullOrWhiteSpace(paciente.CpfPaciente))
                    {
                        TempData[Constantes.MensagemAlerta] = Constantes.CpfExiste;
                    }
                    else if (ModelState.IsValid)
                    {
                        db.Pacientes.Add(paciente);
                        db.SaveChanges();
                        db.Dispose();
                        return RedirectToAction("Index");
                    }
                }
                return View(pPaciente);
            }
            catch (Exception ex)
            {
                TempData[Constantes.MensagemAlerta] = string.Format(Constantes.MensagemErro, ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Paciente paciente = db.Pacientes.Find(id);

            if (paciente == null)
                return HttpNotFound();

            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Paciente pPaciente)
        {
            try
            {
                if (!Uteis.CpfPacienteValido(pPaciente.CpfPaciente))
                {
                    TempData[Constantes.MensagemAlerta] = Constantes.CpfInvalido;
                }
                else
                {
                    Paciente paciente = db.Pacientes.FirstOrDefault(p => p.CpfPaciente == pPaciente.CpfPaciente);

                    if (paciente != null && !string.IsNullOrWhiteSpace(paciente.CpfPaciente))
                    {
                        TempData[Constantes.MensagemAlerta] = Constantes.CpfExiste;
                    }
                    else if (ModelState.IsValid)
                    {
                        db.Entry(paciente).State = EntityState.Modified;
                        db.SaveChanges();
                        db.Dispose();
                        return RedirectToAction("Index");
                    }
                }
                return View(pPaciente);
            }
            catch (Exception ex)
            {
                TempData[Constantes.MensagemAlerta] = string.Format(Constantes.MensagemErro, ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Paciente paciente = db.Pacientes.Find(id);

            if (paciente == null)
                return HttpNotFound();

            return View(paciente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id > 0)
            {
                Paciente paciente = db.Pacientes.Find(id);
                db.Pacientes.Remove(paciente);
                db.SaveChanges();
                db.Dispose();
            }
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
