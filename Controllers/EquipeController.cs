using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_PLAYERS_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace E_PLAYERS_AspNetCore.Controllers
{
    public class EquipeController : Controller
    {
        Equipe equipeModel = new Equipe();

        /// <summary>
        /// Aponta para a Index da View
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }

        /// <summary>
        /// Cadastra as informações inseridas
        /// </summary>
        /// <param name="form">entrada de informações</param>
        /// <returns>redireciona para mesma pagina</returns>
        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe equipe   = new Equipe();
            equipe.IdEquipe = Int32.Parse( form["IdEquipe"]);
            equipe.Nome     = form["Nome"];

            // Inicio Upload Imagem
            // pegando arquivo do form no indice 0
            var file    = form.Files[0];

            // Mapeando a pasta
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

            // Condicional para criação de pasta (se não houver)
            if(file != null)
            {
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }

                // Criando a url do arquivo
                // Captura nome do arquivo = FileName
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                // Cadastra a imagem
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                // Passando nome do arquivo para o objeto
                equipe.Imagem   = file.FileName;
            }
            else
            {
                equipe.Imagem   = "padrao.png";
            }
            // Fim Upload Imagem

            equipeModel.Create(equipe);
            return LocalRedirect("~/Equipe");
        }

        [Route("Equipe/{id}")]
        public IActionResult Excluir(int id)
        {
            equipeModel.Delete(id);
            return LocalRedirect("~/Equipe");

        }

    }
}
