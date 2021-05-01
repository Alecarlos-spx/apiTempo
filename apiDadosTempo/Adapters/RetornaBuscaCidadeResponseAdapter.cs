using apiDadosTempo.DTO;
using apiDadosTempo.Entities;
using apiDadosTempo.Interfaces.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiDadosTempo.Adapters
{
    public class RetornaBuscaCidadeResponseAdapter : IRetornaBuscaCidadeResponseAdapter
    {
        public BuscaCidadeResponse converterCidadeTemperaturaParaResponse(CidadeTemperatura cidade)
        {
            var response = new BuscaCidadeResponse();
            response.cidade = cidade.cidade;
            response.temp = cidade.temp;
            response.min = cidade.min;
            response.max = cidade.max;

            return response;
        }

        public CidadeTemperatura converterResponseParaCidadeTemperatura(BuscaCidadeResponse response)
        {
            var cidade = new CidadeTemperatura();
             cidade.cidade = response.cidade;
             cidade.temp = response.temp;
             cidade.min = response.min;
             cidade.max = response.max;
            cidade.dataHoraConsulta = response.dataHoraConsulta;

            return cidade;
        }
    }
}
