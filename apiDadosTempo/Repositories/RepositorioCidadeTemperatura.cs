using apiDadosTempo.Context;
using apiDadosTempo.Entities;
using apiDadosTempo.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
