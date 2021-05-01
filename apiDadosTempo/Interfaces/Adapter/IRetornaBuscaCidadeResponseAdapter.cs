using apiDadosTempo.Adapters;
using apiDadosTempo.DTO;
using apiDadosTempo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiDadosTempo.Interfaces.Adapter
{
    public interface IRetornaBuscaCidadeResponseAdapter
    {
        public BuscaCidadeResponse converterCidadeTemperaturaParaResponse(CidadeTemperatura cidade);

        public CidadeTemperatura converterResponseParaCidadeTemperatura(BuscaCidadeResponse cidade);


    }
}
