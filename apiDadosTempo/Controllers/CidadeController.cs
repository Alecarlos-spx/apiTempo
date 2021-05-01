using apiDadosTempo.DTO;
using apiDadosTempo.Interfaces.Repositories;
using apiDadosTempo.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiDadosTempo.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CidadeController : ControllerBase
    {
        private readonly IUseCaseCidade _useCaseCidade;

        public CidadeController(IUseCaseCidade useCaseCidade)
        {
            _useCaseCidade = useCaseCidade;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string cidade)
        {
            var response = await _useCaseCidade.Executar(cidade);
            return Ok(response);
        }
    }
}
