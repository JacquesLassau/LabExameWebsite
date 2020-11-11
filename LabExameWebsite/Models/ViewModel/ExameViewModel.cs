using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LabExameWebsite.Models.ViewModel
{
    public class ExameViewModel
    {        
        [Key]
        public int ExameID { get; set; }

        [DisplayName("Tipo de Exame")]
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]        
        public int TipoExameID { get; set; }

        [DisplayName("Nome do Exame")]
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]        
        [StringLength(100, ErrorMessage = "São permitidos até 100 caracteres.")]
        public string NomeExame { get; set; }

        [DisplayName("Observações")]
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [StringLength(1000, ErrorMessage = "São permitidos até 1000 caracteres.")]
        public string ObservacaoExame { get; set; }
               
        public List<SelectListItem> ListarTiposExamesViewModel { get; set; }

        public ExameViewModel()
        {
            ListarTiposExamesViewModel = new List<SelectListItem>();
        }
    }
}