using apiDadosTempo.Context;
using apiDadosTempo.DTO;
using apiDadosTempo.Interfaces.Adapter;
using apiDadosTempo.Interfaces.Repositories;
using apiDadosTempo.Interfaces.UseCases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static apiDadosTempo.Entities.ApiTempoDados;

namespace apiDadosTempo.UseCase
{
    public class UseCaseCidade : IUseCaseCidade
    {
        private readonly IRepositorioCidadeTemperatura _repositorio;
        private readonly IRetornaBuscaCidadeResponseAdapter _adapter;
        private readonly IConsultarDadosTemperaturaCidadeApi _consultaCidade;

        public UseCaseCidade(IRepositorioCidadeTemperatura repositorio, IRetornaBuscaCidadeResponseAdapter adapter, IConsultarDadosTemperaturaCidadeApi consultaCidade)
        {
            _repositorio = repositorio;
            _adapter = adapter;
            _consultaCidade = consultaCidade;
        }

        public async Task<BuscaCidadeResponse> Executar(string cidade)
        {
            
            var response = new BuscaCidadeResponse();

            var cidadeRetorno = _repositorio.GetCidade(cidade);


            if (cidadeRetorno == null)
            {
                var novacidade = await _consultaCidade.ConsultarDadosTemperaturaCidade(cidade);
                if (novacidade.Cidade == null)
                {
                    novacidade.msg = "erro ao buscar cidade";
                    return novacidade;
                }
                var cidadeModelo = _adapter.converterResponseParaCidadeTemperatura(novacidade);
                _repositorio.Add(cidadeModelo);
                return novacidade;
            }


            if (cidadeRetorno.Cidade.ToUpper() == cidade.ToUpper())
            {
                DateTime dataAtual = DateTime.Now;

                if (ComparaDatas(dataAtual, cidadeRetorno.DataHoraConsulta)){

                        response = _adapter.converterCidadeTemperaturaParaResponse(cidadeRetorno);
                        return response;

                }
                else
                {
                    var novacidade = await _consultaCidade.ConsultarDadosTemperaturaCidade(cidade);
                    var cidadeModelo = _adapter.converterResponseParaCidadeTemperatura(novacidade);
                    _repositorio.Update(cidadeModelo, cidadeRetorno.Id);
                    return novacidade;
                }


                //if (dataAtual == cidadeRetorno.DataHoraConsulta)
                //{
                //    if (dataAtual.Hour == cidadeRetorno.DataHoraConsulta.Hour && dataAtual.Minute <= (cidadeRetorno.DataHoraConsulta.Minute + 20))
                //    {

                //    }

                //}


            }
            response = _adapter.converterCidadeTemperaturaParaResponse(cidadeRetorno);
            return response;
        }



        public Boolean ComparaDatas(DateTime dataAtual, DateTime dataBanco)
        {
            if (DateTime.Compare(dataBanco.AddMinutes(20), dataAtual) == -1)
            {
                return false;
            }
            return true;
        }




        public async Task<BuscaCidadeResponse> ConsultarDadosTemperaturaCidade(string request)
        {
            var response = new BuscaCidadeResponse();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var novacidade = await client.GetAsync($"/data/2.5/weather?q={request}&appid=1a788676b927fd9d836e736fd6e92e25&units=metric");
                    novacidade.EnsureSuccessStatusCode();

                    var stringResult = await novacidade.Content.ReadAsStringAsync();

                    var alternativa = JsonConvert.DeserializeObject<Rootobject>(stringResult);

                    response.Cidade = request;
                    response.Temp = alternativa.main.temp;
                    response.Min = alternativa.main.temp_min;
                    response.Max = alternativa.main.temp_max;
                    response.DataHoraConsulta = DateTime.Now;


                    //response = 
                    return response;
                    //    (new
                    //{
                    //    Temp = rawWeather.Main.Temp,
                    //    Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                    //    City = rawWeather.Name
                    //});
                }
                catch (HttpRequestException httpRequestException)
                {
                    return response;
                    //BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }

        }
    }
}
