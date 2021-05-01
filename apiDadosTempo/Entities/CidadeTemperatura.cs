using System;
using System.ComponentModel.DataAnnotations;

namespace apiDadosTempo.Entities
{
    public class CidadeTemperatura
    {
        [Key]
        public int id { get; set; }
        public string cidade { get; set; }
     
        public double temp { get; set; }
   
        public double min { get; set; }
     
        public double max { get; set; }
        public DateTime dataHoraConsulta { get; set; }

    }
}
