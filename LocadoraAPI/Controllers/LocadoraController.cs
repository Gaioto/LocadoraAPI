﻿using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using LocadoraAPI.Models;

namespace LocadoraAPI.Controllers
{
    public class LocadoraController : ApiController
    {
        private static List<Cliente> clientes = new List<Cliente>();
        private static List<Filme> filmes = new List<Filme>();

        [HttpGet]
        public List<Cliente> BuscarClientes()
        {
            return clientes;
        }

        [HttpPost]
        public IHttpActionResult CadastrarCliente(string nome, string filmeAtual = null)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                bool existe = clientes.Exists(x => x.nomeCliente.Equals(nome));

                if (!existe)
                {
                    clientes.Add(new Cliente(nome, filmeAtual));
                    return Ok("Cliente cadastrado");
                }
                
            }
            return NotFound();
        }

        [HttpGet]
        public List<Filme> BuscarFilmes()
        {
            return filmes;
        }

        [HttpPost]
        public IHttpActionResult CadastrarFilme(string nomeFilme, string genero)
        {
            if (!string.IsNullOrEmpty(nomeFilme))
            {
                bool existe = filmes.Exists(x => x.nomeFilme.Equals(nomeFilme));

                if (!existe)
                {
                    filmes.Add(new Filme(nomeFilme, genero));
                    return Ok("Filme cadastrado");
                }
                return Ok("Por favor digite as informações do filme");
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult LocarFilme(string filme, string cliente)
        {
            for (var c = 0; c < clientes.Count; c++)
            {
                if (clientes[c].nomeCliente == cliente)
                {
                    for (var i = 0; i < filmes.Count; i++)
                    {
                        if (filmes[i].nomeFilme == filme)
                        {
                            if (!filmes[i].locado)
                            {
                                filmes[i].locado = true;
                                filmes[i].nomeLocador = cliente;
                                clientes[c].filmeLocado = filmes[i].nomeFilme;
                                return Ok("Filme Locado, por favor devolva dentro do prazo de 5 dias");
                                
                            }
                            return Ok("Filme indisponivel");
                        } 
                        return Ok("Filme não existe no sistema");  
                    }
                } 
            }
            return Ok("Cliente não existe no sistema");
        }

        [HttpPost]
        public IHttpActionResult DevolverFilme(string filme, int diasLocados)
        {

            for (var i = 0; i < filmes.Count; i++)
            {
                if (filmes[i].nomeFilme == filme)
                {
                    clientes.Where(x => x.nomeCliente == filmes[i].nomeLocador).ToList().ForEach(f => f.filmeLocado = null);
                    filmes[i].locado = false;
                    filmes[i].nomeLocador = null;

                    if (diasLocados > 5)
                    {                    
                        return Ok("Filme devolvido, porém atrasado");
                    }
                    else
                    {
                        return Ok("Filme devolvido");
                    }
                }
            }

            return Ok("Cliente não encontrado no sistema");
        }

    }

}

