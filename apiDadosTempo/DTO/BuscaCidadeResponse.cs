using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiDadosTempo.DTO
{
    public class BuscaCidadeResponse
    {
        public string Cidade { get; set; }
     
        public double Temp { get; set; }
      
        public double Min { get; set; }
     
        public double Max { get; set; }
        public DateTime DataHoraConsulta { get; set; }
    }
}
