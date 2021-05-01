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

        public async Task<BuscaCidadeResponse> Add(BuscaCidadeRequest request)
        {
            var response = new BuscaCidadeResponse();

            var cidadeRetorno = _context.cidadeTemperatura.Where(c => c.cidade.Contains(request.Cidade)).FirstOrDefault();


            if (cidadeRetorno == null)
            {
                var novacidade = await ConsultarDadosTemperaturaCidade(request);

                var cidadeModelo = _adapter.converterResponseParaCidadeTemperatura(novacidade);

                _context.cidadeTemperatura.Add(cidadeModelo);
                _context.SaveChanges();

                return novacidade;
            }

                
            if(cidadeRetorno.cidade == request.Cidade)
            {
                DateTime horarioAtual = DateTime.Now;

                if(horarioAtual.Hour == cidadeRetorno.dataHoraConsulta.Hour && horarioAtual.Minute <= (cidadeRetorno.dataHoraConsulta.Minute + 20))
                {
                    response = _adapter.converterCidadeTemperaturaParaResponse(cidadeRetorno);
                    return response;

                }
            }

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


        public async Task<BuscaCidadeResponse> ConsultarDadosTemperaturaCidade(BuscaCidadeRequest request)
        {
            var response = new BuscaCidadeResponse();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var novacidade = await client.GetAsync($"/data/2.5/weather?q={request.Cidade}&appid=1a788676b927fd9d836e736fd6e92e25&units=metric");
                    novacidade.EnsureSuccessStatusCode();

                    var stringResult = await novacidade.Content.ReadAsStringAsync();

                    var alternativa = JsonConvert.DeserializeObject<Rootobject>(stringResult);

                    response.cidade = request.Cidade;
                    response.temp = alternativa.main.temp;
                    response.min = alternativa.main.temp_min;
                    response.max = alternativa.main.temp_max;
                    response.dataHoraConsulta = DateTime.Now;


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
