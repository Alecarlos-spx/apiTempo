using apiDadosTempo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiDadosTempo.Interfaces.UseCases
{
    public interface IUseCaseCidade
    {
        Task<BuscaCidadeResponse> Executar(string cidade);
    }
}
