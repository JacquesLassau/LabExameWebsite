using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
using LabExameWebsite.Models;
using LabExameWebsite.BLL;
using X.PagedList;

namespace LabExameWebsite.Controllers
{
    public class PacienteController : Controller
    {
        private Context db = new Context();

        [HttpGet]
        public ActionResult Index(int pagina = 1)
        {
            var pacientes = db.Pacientes.ToList().ToPagedList(pagina, 5);
            return View(pacientes);
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
        public ActionResult Create(Paciente paciente)
        {
            PacienteValido pacienteValido = new PacienteValido();
            if (!pacienteValido.CpfPacienteValido(paciente.CpfPaciente))
            {
                TempData[Constantes.MensagemAlerta] = "O CPF digitado é inválido ou não existe.";
            }
            else
            {
                var resultCpfExistente = db.Pacientes.FirstOrDefault(p => p.CpfPaciente == paciente.CpfPaciente);

                if (resultCpfExistente != null)
                {
                    TempData[Constantes.MensagemAlerta] = "O CPF digitado está vinculado a outro paciente.";
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        db.Pacientes.Add(paciente);
                        db.SaveChanges();
                        db.Dispose();
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(paciente);
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
        public ActionResult Edit(Paciente paciente)
        {
            PacienteValido pacienteValido = new PacienteValido();
            if (!pacienteValido.CpfPacienteValido(paciente.CpfPaciente))
            {
                TempData[Constantes.MensagemAlerta] = "O CPF digitado é inválido ou não existe.";
            }
            else
            {
                var resultCpfExistente = db.Pacientes.FirstOrDefault(p => p.CpfPaciente == paciente.CpfPaciente && p.PacienteID != paciente.PacienteID);

                if (resultCpfExistente != null)
                {
                    TempData[Constantes.MensagemAlerta] = "O CPF digitado está vinculado a outro paciente.";
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(paciente).State = EntityState.Modified;
                        db.SaveChanges();
                        db.Dispose();
                        return RedirectToAction("Index");
                    }
                }                
            }

            return View(paciente);
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
            Paciente paciente = db.Pacientes.Find(id);
            db.Pacientes.Remove(paciente);
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
