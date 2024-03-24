using System.ComponentModel.DataAnnotations;

namespace ControleStoke.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o Nome de Login Correto")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o Nome de Senha")]
        public string Senha { get; set; }

    }
}
