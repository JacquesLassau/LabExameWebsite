using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LabExameWebsite.Models
{
    public class TipoExame
    {
        public TipoExame() { this.Exames = new HashSet<Exame>(); }
        public virtual ICollection<Exame> Exames { get; set; }


        [Key]
        public int TipoExameID { get; set; }

        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Tipo de Exame")]
        [StringLength(100, ErrorMessage = "São permitidos até 100 caracteres.")]
        public string NomeTipoExame { get; set; }

        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Descrição")]
        [StringLength(256, ErrorMessage = "São permitidos até 250 caracteres.")]
        public string DescricaoTipoExame { get; set; }        
    }
}