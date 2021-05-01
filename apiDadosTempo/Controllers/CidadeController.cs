using apiDadosTempo.DTO;
using apiDadosTempo.Interfaces.Repositories;
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
        private readonly IRepositorioCidadeTemperatura _repositorio;

        public CidadeController(IRepositorioCidadeTemperatura repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpPost]
        public IActionResult Post([FromBody]BuscaCidadeRequest cidade)
        {
            var response = _repositorio.Add(cidade);
            return Ok(response);
        }
    }
}
