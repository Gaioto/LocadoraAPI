using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraAPI.Models
{
    public class Filme
    {

        public string nomeFilme { get; set; }

        public string generoFilme { get; set; }

        public string nomeLocador { get; set; }

        public bool locado { get; set; }


        public Filme()
        {
                
        }

        public Filme(string nome, string genero, string locador ,bool locado)
        {
            this.nomeFilme = nome;
            this.generoFilme = genero;
            this.nomeLocador = locador;
            this.locado = locado;
        }

    }
}