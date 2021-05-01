using apiDadosTempo.Context;
using apiDadosTempo.DTO;
using apiDadosTempo.Entities;
using apiDadosTempo.Interfaces.Adapter;
using apiDadosTempo.Interfaces.Repositories;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static apiDadosTempo.Entities.ApiTempoDados;

namespace apiDadosTempo.Repositories
{
    public class RepositorioCidadeTemperatura : IRepositorioCidadeTemperatura
    {
        private readonly ContextData _context;
        private readonly IRetornaBuscaCidadeResponseAdapter _adapter;

        public RepositorioCidadeTemperatura(ContextData context, IRetornaBuscaCidadeResponseAdapter adapter)
        {
            _context = context;
            _adapter = adapter;
        }

        public async Task<BuscaCidadeResponse> Add(string request)
        {
            var response = new BuscaCidadeResponse();

            var cidadeRetorno = _context.cidadeTemperatura.Where(c => c.Cidade.Contains(request)).FirstOrDefault();


            if (cidadeRetorno == null)
            {
                var novacidade = await ConsultarDadosTemperaturaCidade(request);

                var cidadeModelo = _adapter.converterResponseParaCidadeTemperatura(novacidade);

                _context.cidadeTemperatura.Add(cidadeModelo);
                _context.SaveChanges();

                return novacidade;
            }

                
            if(cidadeRetorno.Cidade == request)
            {
                DateTime dataAtual = DateTime.Now;

                if (dataAtual == cidadeRetorno.DataHoraConsulta)
                {
                    if(dataAtual.Hour == cidadeRetorno.DataHoraConsulta.Hour && dataAtual.Minute <= (cidadeRetorno.DataHoraConsulta.Minute + 20))
                    {
                        response = _adapter.converterCidadeTemperaturaParaResponse(cidadeRetorno);
                        return response;

                    }

                }

             
            }
            response = _adapter.converterCidadeTemperaturaParaResponse(cidadeRetorno);
            return response;

            
        }

        public CidadeTemperatura GetCidade(string request)
        {
            throw new NotImplementedException();
        }

        public string Update(CidadeTemperatura request)
        {
            throw new NotImplementedException();
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
