using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiDadosTempo.DTO
{
    public class BuscaCidadeResponse
    {
        public string cidade { get; set; }
     
        public double temp { get; set; }
      
        public double min { get; set; }
     
        public double max { get; set; }
        public DateTime dataHoraConsulta { get; set; }
    }
}
