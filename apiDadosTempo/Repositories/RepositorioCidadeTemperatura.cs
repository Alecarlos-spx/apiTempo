﻿using apiDadosTempo.Context;
using apiDadosTempo.DTO;
using apiDadosTempo.Entities;
using apiDadosTempo.Interfaces.Adapter;
using apiDadosTempo.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
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


        public RepositorioCidadeTemperatura(ContextData context)
        {
            _context = context;
      
        }

        public void Add(CidadeTemperatura request)
        {
               _context.cidadeTemperatura.Add(request);
               _context.SaveChanges();
        }

        public CidadeTemperatura GetCidade(string request)
        {
            return _context.cidadeTemperatura.Where(c => c.Cidade.Contains(request)).FirstOrDefault();
             
        }

        public void Update(CidadeTemperatura request, int id)
        {
            request.Id = id;
            var local = _context.Set<CidadeTemperatura>().Local.Where(x => x.Id == id).FirstOrDefault();
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            //_context.Attach<CidadeTemperatura>(local);

            _context.cidadeTemperatura.Update(request);
            _context.SaveChanges();
        }


       

       



    }
}
