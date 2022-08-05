/*
 * 
 * Analista: Jacques de Lassau
 * Data: 04/08/2022 23:08h
 * Modificações: Action de Logout não estava sendo usada. Mecanismos de autenticação ainda não foram criados, basta clicar em "entrar" para a página inicial.
 * 
 */
using System.Web.Mvc;

namespace LabExameWebsite.Controllers
{
    public class AreaRestritaController : Controller
    {        
        public ActionResult Login()
        {
            return View();
        }          
    }
}