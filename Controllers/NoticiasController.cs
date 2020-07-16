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
    public class NoticiasController : Controller
    {
        /// <summary>
        /// Aponta para a Index da View
        /// </summary>
        /// <returns>View</returns>
        Noticias noticiasModel = new Noticias();
        public IActionResult Index()
        {
            ViewBag.Noticias = noticiasModel.ReadAll();
            return View();
        }

        /// <summary>
        /// Publica as informações inseridas
        /// </summary>
        /// <param name="form">entrada de informações</param>
        /// <returns>redireciona para mesma pagina</returns>
        public IActionResult Publicar(IFormCollection form)
        {
            Noticias noticias = new Noticias();
            noticias.IdNoticia = Int32.Parse( form["IdNoticia"]);
            noticias.Titulo   = form["Titulo"];
            noticias.Texto    = form["Texto"];
            // Inicio Upload Imagem
            // pegando arquivo do form no indice 0
            var file    = form.Files[0];

            // Mapeando a pasta
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticias");

            // Condicional para criação de pasta (se não houver)
            if(file != null)
            {
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }

                // Criando a url do arquivo
                // Captura nome do arquivo = FileName
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticias", folder, file.FileName);
                // Cadastra a imagem
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                // Passando nome do arquivo para o objeto
                noticias.Imagem   = file.FileName;
            }
            else
            {
                noticias.Imagem   = "padrao.png";
            }
            // Fim Upload Imagem

            noticiasModel.Create(noticias);
            return LocalRedirect("~/Noticias");
        }

        [Route("Noticias/{id}")]
        public IActionResult Excluir(int id)
        {
            noticiasModel.Delete(id);
            return LocalRedirect("~/Noticias");

        }

    }
}