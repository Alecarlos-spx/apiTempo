using apiDadosTempo.DTO;
using apiDadosTempo.Entities;
using apiDadosTempo.Interfaces.Adapter;

namespace apiDadosTempo.Adapters
{
    public class RetornaBuscaCidadeResponseAdapter : IRetornaBuscaCidadeResponseAdapter
    {
        public BuscaCidadeResponse converterCidadeTemperaturaParaResponse(CidadeTemperatura cidade)
        {
            var response = new BuscaCidadeResponse();
            response.Cidade = cidade.Cidade;
            response.Temp = cidade.Temp;
            response.Min = cidade.Min;
            response.Max = cidade.Max;
            response.DataHoraConsulta = cidade.DataHoraConsulta;

            return response;
        }

        public CidadeTemperatura converterResponseParaCidadeTemperatura(BuscaCidadeResponse response)
        {
            var cidade = new CidadeTemperatura();
             cidade.Cidade = response.Cidade;
             cidade.Temp = response.Temp;
             cidade.Min = response.Min;
             cidade.Max = response.Max;
            cidade.DataHoraConsulta = response.DataHoraConsulta;

            return cidade;
        }
    }
}
