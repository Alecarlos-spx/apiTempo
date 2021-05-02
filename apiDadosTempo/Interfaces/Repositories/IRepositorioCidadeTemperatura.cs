using apiDadosTempo.DTO;
using apiDadosTempo.Entities;
using System.Threading.Tasks;

namespace apiDadosTempo.Interfaces.Repositories
{
    public interface IRepositorioCidadeTemperatura
    {
        public void Add(CidadeTemperatura request);
        public void Update(CidadeTemperatura request, int id);
        public CidadeTemperatura GetCidade(string request);

    }
}
