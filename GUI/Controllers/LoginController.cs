using ControleStoke.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ControleStoke.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(loginModel.Login == "dany" && loginModel.Senha == "123")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                    TempData["MenssagemErro"] = $"Usuário e/ou Senha inválido (s). Por favor, tente novamente.";
                                     
                }
                return View("Index");
            }
            catch (Exception erro){
                TempData["MenssagemErro"] = $"Opssss, Não consegui te achar, erro: {erro.Message}";
                return RedirectToAction("Index");

            }
        }


    }
}
