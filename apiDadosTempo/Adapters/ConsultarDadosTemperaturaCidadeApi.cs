using apiDadosTempo.DTO;
using apiDadosTempo.Interfaces.Adapter;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static apiDadosTempo.Entities.ApiTempoDados;

namespace apiDadosTempo.Adapters
{
    public class ConsultarDadosTemperaturaCidadeApi : IConsultarDadosTemperaturaCidadeApi
    {

        private readonly IConfiguration _configuration;

        public ConsultarDadosTemperaturaCidadeApi(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<BuscaCidadeResponse> ConsultarDadosTemperaturaCidade(string request)
        {
            var response = new BuscaCidadeResponse();
            var keyOpenWeather = _configuration.GetSection("keyOpenWeather");



            


            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var novacidade = await client.GetAsync($"/data/2.5/weather?q={request}&appid={keyOpenWeather.Value}&units=metric");
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
