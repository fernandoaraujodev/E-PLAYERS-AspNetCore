using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_PLAYERS_AspNetCore.Models;
using Microsoft.AspNetCore.Http;

namespace E_PLAYERS_AspNetCore.Controllers
{
    public class EquipeController : Controller
    {
        Equipe equipeModel = new Equipe();
        public IActionResult Index()
        {
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }

        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe equipe   = new Equipe();
            equipe.IdEquipe = Int32.Parse( form["IdEquipe"]);
            equipe.Nome     = form["Nome"];
            equipe.Imagem   = form["Imagem"];

            equipeModel.Create(equipe);
            
            ViewBag.Equipes = equipeModel.ReadAll();
            return LocalRedirect("~/Equipe");
        }

    }
}
