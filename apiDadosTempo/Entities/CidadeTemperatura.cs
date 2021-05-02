using System;
using System.ComponentModel.DataAnnotations;

namespace apiDadosTempo.Entities
{
    public class CidadeTemperatura
    {
        [Key]
        public int Id { get; set; }
        public string Cidade { get; set; }
     
        public double Temp { get; set; }
   
        public double Min { get; set; }
     
        public double Max { get; set; }
        public DateTime DataHoraConsulta { get; set; }

    }
}
