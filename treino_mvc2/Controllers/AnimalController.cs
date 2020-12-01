using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using treino_mvc2.Data;
using treino_mvc2.Models;

namespace treino_mvc2.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {
        private readonly ApplicationDbContext database;
        public AnimalController(ApplicationDbContext database)
        {
            this.database = database;
        }

        public IActionResult Listar() //listar os animais
        {
            var animais = database.Animal.ToList();
            return View(animais);
        }

        [Authorize(Policy = "TipoUser")] //so acessa quem for doar
         //so acessa quem for veterinario
        public IActionResult Cadastrar() //formulario
        {
            return View();
        }

        [Authorize(Policy = "TipoVet")] //so veterinario acessa
        public IActionResult ListarEditar() //listar os animais
        {
            var animais = database.Animal.ToList();
            return View(animais);
        }

        [Authorize(Policy = "TipoVet")]
        public IActionResult Editar(int id)
        {
            Animais animais = database.Animal.First(registro => registro.Id == id);
            return View("Cadastrar", animais); //usando o mesmo formulario cadastrar
        }

        [Authorize(Policy = "TipoVet")]
        public IActionResult Deletar(int id)
        {
            Animais animais = database.Animal.First(registro => registro.Id == id); 
            database.Animal.Remove(animais);
            database.SaveChanges(); 
            return RedirectToAction("Listar"); //deleta um animal
        }

        [HttpPost] //essa action esta sendo acessada pelo formulario de cadastrar animal
        public IActionResult Salvar(Animais animais) 
        {
            //salvando as informacoes no BD na tabela animal
            if(animais.Id == 0)
            {
                //salve um novo animal
                database.Animal.Add(animais);
            }
            else
            {
                //atualizar um animal
                Animais animalDoBanco = database.Animal.First(registro => registro.Id == animais.Id);
                
                animalDoBanco.Nome = animais.Nome;
                animalDoBanco.Raca = animais.Raca;
                animalDoBanco.Idade = animais.Idade;
                animalDoBanco.Telefone = animais.Telefone;
                animalDoBanco.Comentario = animais.Comentario;
            }
            
            
            database.SaveChanges();
            
            return RedirectToAction("Listar");
        }

       
    }
}