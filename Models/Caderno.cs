using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Caderno
    {       
        public int Id { get; set; }

        [Required]        
        public string Titulo { get; set; }

        [Required]
        [Range(0, 999999.99)]
        public decimal Valor { get; set; }

        [Required]
        [Range(0, 99999)] 
        public int NrFolhas { get; set; }

        public Caderno()
        {
        }

        public void copiarDados(Models.Caderno caderno)
        {
            this.Titulo = caderno.Titulo;
            this.Valor = caderno.Valor;
            this.NrFolhas = caderno.NrFolhas;
        }
    }
}