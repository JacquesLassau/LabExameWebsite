using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabExameWebsite.Models
{
    public class Exame
    {
        public Exame() { this.Agendamentos = new HashSet<Agendamento>(); }
        public virtual ICollection<Agendamento> Agendamentos { get; set; }


        [Key]
        public int ExameID { get; set; }       

        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]        
        [StringLength(100, ErrorMessage = "São permitidos até 100 caracteres.")]
        public string NomeExame { get; set; }

        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]        
        [StringLength(1000, ErrorMessage = "São permitidos até 1000 caracteres.")]
        public string ObservacaoExame { get; set; }

        [ForeignKey("TipoExame")]
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        public int TipoExameID { get; set; }

        public virtual TipoExame TipoExame { get; set; }       
    }
}