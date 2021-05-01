using apiDadosTempo.DTO;
using apiDadosTempo.Entities;
using System.Threading.Tasks;

namespace apiDadosTempo.Interfaces.Repositories
{
    public interface IRepositorioCidadeTemperatura
    {
        public Task<BuscaCidadeResponse> Add(string request);
        public string Update(CidadeTemperatura request);
        public CidadeTemperatura GetCidade(string request);

    }
}
