using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using LabExameWebsite.Models;
using LabExameWebsite.Models.ViewModel;
using X.PagedList;

namespace LabExameWebsite.Controllers
{
    public class AgendamentoController : Controller
    {
        private Context db = new Context();

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResultCustomizado
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        [HttpGet]
        public ActionResult Monitor(int pagina = 1)
        {
            var agendamentos = db.Agendamentos.Include(p => p.Paciente).Include(p => p.Exame).ToList().ToPagedList(pagina, 5);
            return View(agendamentos);
        }

        [HttpGet]
        public ActionResult Index(int pagina = 1)
        {
            var agendamentos = db.Agendamentos.Include(p => p.Paciente).Include(p => p.Exame).ToList().ToPagedList(pagina, 5);
            return View(agendamentos);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Agendamento agendamento = db.Agendamentos.Find(id);

            if (agendamento == null)
                return HttpNotFound();

            AgendamentoViewModel agendamentoViewModel = new AgendamentoViewModel();

            var tipoExames = db.TiposExames.ToList();
            var exames = db.Exames.ToList();

            foreach (var item in tipoExames)
            {
                agendamentoViewModel.ListarTiposExameAgendamentoViewModel.Add(new SelectListItem()
                {
                    Text = Convert.ToString(item.TipoExameID) + " - " + item.NomeTipoExame,
                    Value = Convert.ToString(item.TipoExameID),
                    Selected = true
                });
            }

            foreach (var item in exames)
            {
                agendamentoViewModel.ListarExamesAgendamentoViewModel.Add(new SelectListItem()
                {
                    Text = Convert.ToString(item.ExameID) + " - " + item.NomeExame,
                    Value = Convert.ToString(item.ExameID),
                    Selected = true
                });
            }

            Paciente paciente = db.Pacientes.Find(agendamento.PacienteID);
            Exame exame = db.Exames.Find(agendamento.ExameID);
            TipoExame tipoExame = db.TiposExames.Find(exame.TipoExameID);

            int ano = agendamento.DataHoraAgendamento.Year;
            int mes = agendamento.DataHoraAgendamento.Month;
            int dia = agendamento.DataHoraAgendamento.Day;
            int hor = agendamento.DataHoraAgendamento.Hour;
            int min = agendamento.DataHoraAgendamento.Minute;
            DateTime agendamentoAgendamento = new DateTime(ano, mes, dia, hor, min, 00);

            agendamentoViewModel.DataHoraAgendamento = agendamentoAgendamento;
            agendamentoViewModel.HorasAgendamento = hor;
            agendamentoViewModel.MinutosAgendamento = min;
            agendamentoViewModel.AgendamentoID = agendamento.AgendamentoID;
            agendamentoViewModel.PacienteID = agendamento.PacienteID;
            agendamentoViewModel.NomePacienteSelecionado = paciente.NomePaciente;
            agendamentoViewModel.CpfPacienteAgendamento = paciente.CpfPaciente;
            agendamentoViewModel.TipoExameIDAgendamento = tipoExame.TipoExameID;
            agendamentoViewModel.ExameID = exame.ExameID;

            return View(agendamentoViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AgendamentoViewModel tipoExameAgendamentoViewModel = new AgendamentoViewModel();
            var tipoExamesAgendamento = db.TiposExames.ToList();

            foreach (var item in tipoExamesAgendamento)
            {
                tipoExameAgendamentoViewModel.ListarTiposExameAgendamentoViewModel.Add(new SelectListItem()
                {
                    Text = Convert.ToString(item.TipoExameID) + " - " + item.NomeTipoExame,
                    Value = Convert.ToString(item.TipoExameID),
                    Selected = true
                });
            }

            return View(tipoExameAgendamentoViewModel);
        }

        [HttpGet]
        public JsonResult AgendamentorPacienteCpfJR(string cpfPesquisaPaciente)
        {
            var result = db.Pacientes.FirstOrDefault(p => p.CpfPaciente == cpfPesquisaPaciente);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AgendamentorListaExamesJR(int idTipoExame)
        {
            var result = db.Exames.Where(p => p.TipoExameID == idTipoExame).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Agendamento agendamento)
        {
            agendamento.ProtocoloAgendamento = Constantes.ProtocoloAleatorio;

            int ano = agendamento.DataHoraAgendamento.Year;
            int mes = agendamento.DataHoraAgendamento.Month;
            int dia = agendamento.DataHoraAgendamento.Day;
            int hor = agendamento.HorasAgendamento;
            int min = agendamento.MinutosAgendamento;

            DateTime agendamentoAgendamento = new DateTime(ano, mes, dia, hor, min, 00);
            agendamento.DataHoraAgendamento = agendamentoAgendamento;

            if (ModelState.IsValid)
            {
                var result = db.Agendamentos.FirstOrDefault(p => p.DataHoraAgendamento == agendamento.DataHoraAgendamento);

                if (result != null)
                {
                    TempData[Constantes.MensagemAlerta] = "Já existe uma agendamento nesse horário.";
                }
                else
                {
                    db.Agendamentos.Add(agendamento);
                    db.SaveChanges();
                    db.Dispose();
                    return RedirectToAction("Index");
                }
            }

            return View(agendamento);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Agendamento agendamento = db.Agendamentos.Find(id);

            if (agendamento == null)
                return HttpNotFound();

            AgendamentoViewModel agendamentoViewModel = new AgendamentoViewModel();

            var tipoExames = db.TiposExames.ToList();
            var exames = db.Exames.ToList();

            foreach (var item in tipoExames)
            {
                agendamentoViewModel.ListarTiposExameAgendamentoViewModel.Add(new SelectListItem()
                {
                    Text = Convert.ToString(item.TipoExameID) + " - " + item.NomeTipoExame,
                    Value = Convert.ToString(item.TipoExameID),
                    Selected = true
                });
            }

            foreach (var item in exames)
            {
                agendamentoViewModel.ListarExamesAgendamentoViewModel.Add(new SelectListItem()
                {
                    Text = Convert.ToString(item.ExameID) + " - " + item.NomeExame,
                    Value = Convert.ToString(item.ExameID),
                    Selected = true
                });
            }

            Paciente paciente = db.Pacientes.Find(agendamento.PacienteID);
            Exame exame = db.Exames.Find(agendamento.ExameID);
            TipoExame tipoExame = db.TiposExames.Find(exame.TipoExameID);

            int ano = agendamento.DataHoraAgendamento.Year;
            int mes = agendamento.DataHoraAgendamento.Month;
            int dia = agendamento.DataHoraAgendamento.Day;
            int hor = agendamento.DataHoraAgendamento.Hour;
            int min = agendamento.DataHoraAgendamento.Minute;
            DateTime agendamentoAgendamento = new DateTime(ano, mes, dia, hor, min, 00);

            agendamentoViewModel.DataHoraAgendamento = agendamentoAgendamento;
            agendamentoViewModel.HorasAgendamento = hor;
            agendamentoViewModel.MinutosAgendamento = min;
            agendamentoViewModel.AgendamentoID = agendamento.AgendamentoID;
            agendamentoViewModel.PacienteID = agendamento.PacienteID;
            agendamentoViewModel.NomePacienteSelecionado = paciente.NomePaciente;
            agendamentoViewModel.CpfPacienteAgendamento = paciente.CpfPaciente;
            agendamentoViewModel.TipoExameIDAgendamento = tipoExame.TipoExameID;
            agendamentoViewModel.ExameID = exame.ExameID;

            return View(agendamentoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Agendamento agendamento)
        {
            int ano = agendamento.DataHoraAgendamento.Year;
            int mes = agendamento.DataHoraAgendamento.Month;
            int dia = agendamento.DataHoraAgendamento.Day;
            int hor = agendamento.HorasAgendamento;
            int min = agendamento.MinutosAgendamento;

            DateTime agendamentoAgendamento = new DateTime(ano, mes, dia, hor, min, 00);
            agendamento.DataHoraAgendamento = agendamentoAgendamento;

            if (ModelState.IsValid)
            {
                var result = db.Agendamentos.FirstOrDefault(p => p.DataHoraAgendamento == agendamento.DataHoraAgendamento && p.AgendamentoID != agendamento.AgendamentoID);

                if (result != null)
                {
                    TempData[Constantes.MensagemAlerta] = "Já existe uma agendamento nesse horário.";
                }
                else
                {
                    db.Entry(agendamento).State = EntityState.Modified;
                    db.SaveChanges();
                    db.Dispose();
                    return RedirectToAction("Index");
                }
            }
            return View(agendamento);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Agendamento agendamento = db.Agendamentos.Find(id);

            if (agendamento == null)
                return HttpNotFound();

            AgendamentoViewModel agendamentoViewModel = new AgendamentoViewModel();

            var tipoExames = db.TiposExames.ToList();
            var exames = db.Exames.ToList();

            foreach (var item in tipoExames)
            {
                agendamentoViewModel.ListarTiposExameAgendamentoViewModel.Add(new SelectListItem()
                {
                    Text = Convert.ToString(item.TipoExameID) + " - " + item.NomeTipoExame,
                    Value = Convert.ToString(item.TipoExameID),
                    Selected = true
                });
            }

            foreach (var item in exames)
            {
                agendamentoViewModel.ListarExamesAgendamentoViewModel.Add(new SelectListItem()
                {
                    Text = Convert.ToString(item.ExameID) + " - " + item.NomeExame,
                    Value = Convert.ToString(item.ExameID),
                    Selected = true
                });
            }

            Paciente paciente = db.Pacientes.Find(agendamento.PacienteID);
            Exame exame = db.Exames.Find(agendamento.ExameID);
            TipoExame tipoExame = db.TiposExames.Find(exame.TipoExameID);

            int ano = agendamento.DataHoraAgendamento.Year;
            int mes = agendamento.DataHoraAgendamento.Month;
            int dia = agendamento.DataHoraAgendamento.Day;
            int hor = agendamento.DataHoraAgendamento.Hour;
            int min = agendamento.DataHoraAgendamento.Minute;
            DateTime agendamentoAgendamento = new DateTime(ano, mes, dia, hor, min, 00);

            agendamentoViewModel.DataHoraAgendamento = agendamentoAgendamento;
            agendamentoViewModel.HorasAgendamento = hor;
            agendamentoViewModel.MinutosAgendamento = min;
            agendamentoViewModel.AgendamentoID = agendamento.AgendamentoID;
            agendamentoViewModel.PacienteID = agendamento.PacienteID;
            agendamentoViewModel.NomePacienteSelecionado = paciente.NomePaciente;
            agendamentoViewModel.CpfPacienteAgendamento = paciente.CpfPaciente;
            agendamentoViewModel.TipoExameIDAgendamento = tipoExame.TipoExameID;
            agendamentoViewModel.ExameID = exame.ExameID;

            return View(agendamentoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agendamento agendamento = db.Agendamentos.Find(id);
            db.Agendamentos.Remove(agendamento);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index");
        }

        [HttpGet]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}