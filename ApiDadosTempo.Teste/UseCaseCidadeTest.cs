using apiDadosTempo.DTO;
using apiDadosTempo.Entities;
using apiDadosTempo.Interfaces.Adapter;
using apiDadosTempo.Interfaces.Repositories;
using apiDadosTempo.UseCase;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ApiDadosTempo.Teste
{
    public class UseCaseCidadeTest
    {

        private readonly Mock<IRepositorioCidadeTemperatura>  _repositorioCidade;
        private readonly Mock<IRetornaBuscaCidadeResponseAdapter> _adapter;
      
        private readonly UseCaseCidade _useCase;
        private readonly Mock<IConsultarDadosTemperaturaCidadeApi> _consultaCidade;
       

        public UseCaseCidadeTest()
        {
            _repositorioCidade = new Mock<IRepositorioCidadeTemperatura>();
            _adapter = new Mock<IRetornaBuscaCidadeResponseAdapter>();
            _consultaCidade = new Mock<IConsultarDadosTemperaturaCidadeApi>();
            _useCase = new UseCaseCidade(_repositorioCidade.Object, _adapter.Object, _consultaCidade.Object);
        }

        [Fact]
        public async void Cidade_AdicionarCidade_QuandoRetornarNovaCidadeSucesso()
        {
          var cidade= "São Paulo";
            var request = new BuscaCidadeResponse()
            {
                Cidade = "São Paulo",
                Temp = 16.5200000,
                Max = 18.585656,
                Min = 15.025252,
                DataHoraConsulta = DateTime.Now
            };

            var cidadeTemperatura = new CidadeTemperatura();
            var response = new BuscaCidadeResponse()
            {
                Cidade = "São Paulo",
                Temp = 16.5200000,
                Max = 18.585656,
                Min = 15.025252,
                DataHoraConsulta = DateTime.Now
            };


            _repositorioCidade.Setup(repositorio => repositorio.GetCidade(cidade));

            _consultaCidade.Setup(consulta => consulta.ConsultarDadosTemperaturaCidade(cidade)).ReturnsAsync(response);

            _adapter.Setup(adapter => adapter.converterResponseParaCidadeTemperatura(response)).Returns(cidadeTemperatura);

            _repositorioCidade.Setup(repositorio => repositorio.Add(cidadeTemperatura));



            //Act

            var result = await _useCase.Executar(cidade);

            //Assert

            response.Should().BeEquivalentTo(result);

        }


        [Fact]
        public async void Cidade_AtualizarCidade_QuandoHorarioMenorQueVinteMinutos()
        {
            var cidade = "São Paulo";
            var request = new BuscaCidadeResponse()
            {
                Cidade = "São Paulo",
                Temp = 16.5200000,
                Max = 18.585656,
                Min = 15.025252,
                DataHoraConsulta = DateTime.Now
            };

            var cidadeTemperatura = new CidadeTemperatura()
            {
                Cidade = "São Paulo",
                Temp = 16.5200000,
                Max = 18.585656,
                Min = 15.025252,
                DataHoraConsulta = DateTime.Now
            }; 
            var response = new BuscaCidadeResponse()
            {
                Cidade = "São Paulo",
                Temp = 16.5200000,
                Max = 18.585656,
                Min = 15.025252,
                DataHoraConsulta = DateTime.Now
            };

            _repositorioCidade.Setup(repositorio => repositorio.GetCidade(cidade)).Returns(cidadeTemperatura);


            _adapter.Setup(adapter => adapter.converterCidadeTemperaturaParaResponse(cidadeTemperatura)).Returns(response);


            //Act

            var result = await _useCase.Executar(cidade);

            //Assert

            response.Should().BeEquivalentTo(result);
        }


        [Fact]
        public async void Cidade_AtualizarCidade_QuandoHorarioMaiorQueVinteMinutos()
        {
            var cidade = "São Paulo";
            var request = new BuscaCidadeResponse()
            {
                Cidade = "São Paulo",
                Temp = 16.5200000,
                Max = 18.585656,
                Min = 15.025252,
                DataHoraConsulta = DateTime.Now
            };

            var cidadeTemperatura = new CidadeTemperatura()
            {
                Cidade = "São Paulo",
                Temp = 16.5200000,
                Max = 18.585656,
                Min = 15.025252,
                DataHoraConsulta = (DateTime.Now).AddMinutes(-20)
            };
            var response = new BuscaCidadeResponse()
            {
                Cidade = "São Paulo",
                Temp = 16.5200000,
                Max = 18.585656,
                Min = 15.025252,
                DataHoraConsulta = DateTime.Now
            };

            _repositorioCidade.Setup(repositorio => repositorio.GetCidade(cidade)).Returns(cidadeTemperatura);


            _adapter.Setup(adapter => adapter.converterCidadeTemperaturaParaResponse(cidadeTemperatura)).Returns(response);





            _adapter.Setup(adapter => adapter.converterResponseParaCidadeTemperatura(response)).Returns(cidadeTemperatura);



            _consultaCidade.Setup(consulta => consulta.ConsultarDadosTemperaturaCidade(cidade)).ReturnsAsync(response);

            _adapter.Setup(adapter => adapter.converterResponseParaCidadeTemperatura(response)).Returns(cidadeTemperatura);

            _repositorioCidade.Setup(repositorio => repositorio.Update(cidadeTemperatura, cidadeTemperatura.Id));



            //Act

            var result = await _useCase.Executar(cidade);

            //Assert

            response.Should().BeEquivalentTo(result);
        }


        [Fact]
        public async void Cidade_AtualizarCidade_QuandoCidadeNaoEncontrada()
        {
            var cidade = "adfgtg";

            var response = new BuscaCidadeResponse()
            {
                msg = "erro ao buscar cidade"

            };

            _repositorioCidade.Setup(repositorio => repositorio.GetCidade(cidade));

            _consultaCidade.Setup(consulta => consulta.ConsultarDadosTemperaturaCidade(cidade)).ReturnsAsync(response);

            


            //Act

            var result = await _useCase.Executar(cidade);

            //Assert

            response.Should().BeEquivalentTo(result);
        }

    }
}
