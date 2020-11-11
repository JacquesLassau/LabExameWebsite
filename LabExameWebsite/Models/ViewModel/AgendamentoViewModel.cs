using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LabExameWebsite.Models.ViewModel
{
    public class AgendamentoViewModel
    {
        [Key]
        public int AgendamentoID { get; set; }

        [DisplayName("Paciente")]
        public int PacienteID { get; set; }

        [DisplayName("Exame")]
        public int ExameID { get; set; }

        [DisplayName("Data da Agendamento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataHoraAgendamento { get; set; } /* {0:dd-MM-yyyy H:mm:ss zzz} DataFormatString Completo */

        [Range(0, 23, ErrorMessage = "Só é permitido entre 0 e 23 horas")]
        public int HorasAgendamento { get; set; }

        [Range(0, 59, ErrorMessage = "Só é permitido entre 0 e 59 minutos")]
        public int MinutosAgendamento { get; set; }

        [DisplayName("Protocolo")]
        public string ProtocoloAgendamento { get; set; }

        [DisplayName("CPF")]
        [StringLength(14, ErrorMessage = "Devem ser incluídos 14 caracteres.")]
        [MaxLength(14, ErrorMessage = "Preenchimento máximo de 14 caracteres.")]
        [MinLength(14, ErrorMessage = "Preenchimento mínimo de 14 caracteres.")]
        [RegularExpression(@"[0-9]{3}\.?[0-9]{3}\.?[0-9]{3}\-?[0-9]{2}", ErrorMessage = "Digite no formato 000.000.000-00")]
        public string CpfPacienteAgendamento { get; set; }

        [DisplayName("Paciente")]
        public string NomePacienteSelecionado { get; set; }

        [DisplayName("Exame")]
        public string NomeExameSelecionado { get; set; }

        [DisplayName("Tipo")]
        public int TipoExameIDAgendamento { get; set; }

        public List<SelectListItem> ListarTiposExameAgendamentoViewModel { get; set; }
        public List<SelectListItem> ListarExamesAgendamentoViewModel { get; set; }

        public AgendamentoViewModel()
        {
            ListarTiposExameAgendamentoViewModel = new List<SelectListItem>();
            ListarExamesAgendamentoViewModel = new List<SelectListItem>();
        }
    }
}