using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabExameWebsite.Models
{
    public class Agendamento
    {
        [Key]
        [DisplayName("Código")]
        public int AgendamentoID { get; set; }

        [ForeignKey("Paciente")]
        [DisplayName("Paciente")]
        public int PacienteID { get; set; }

        [JsonIgnore]
        public virtual Paciente Paciente { get; set; }

        [ForeignKey("Exame")]
        [DisplayName("Exame")]
        public int ExameID { get; set; }

        [JsonIgnore]
        public virtual Exame Exame { get; set; }

        [DisplayName("Data da Agendamento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataHoraAgendamento { get; set; } /* {0:dd-MM-yyyy H:mm:ss zzz} DataFormatString Completo */

        [NotMapped]
        [Range(0, 23, ErrorMessage = "Só é permitido entre 0 e 23 horas")]
        public int HorasAgendamento { get; set; }

        [NotMapped]
        [Range(0, 59, ErrorMessage = "Só é permitido entre 0 e 59 minutos")]
        public int MinutosAgendamento { get; set; }

        [DisplayName("Protocolo")]
        public string ProtocoloAgendamento { get; set; }
    }
}