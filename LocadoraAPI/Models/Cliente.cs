using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraAPI.Models
{
    public class Cliente
    { 

        public string nomeCliente { get; set; }

        public string filmeLocado { get; set; }

        public Cliente()
        {

        }

        public Cliente(string nmCliente, string filme)
        {
            this.nomeCliente = nmCliente;
            this.filmeLocado = filme;
        }
    }
}