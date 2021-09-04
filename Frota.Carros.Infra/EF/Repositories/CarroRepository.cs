using Frota.Carros.Domain.Models.Carro;
using System.Collections.Generic;
using System.Linq;

namespace Frota.Carros.Infra.EF.Repositories
{
    public class CarroRepository : ICarroRepository
    {
        private readonly FrotaCarrosDbContext _context;

        public CarroRepository(FrotaCarrosDbContext context)
        {
            _context = context;
        }

        public void Create(Carro carro)
        {
            _context.Add(carro);
            _context.SaveChanges();
        }

        public void Delete(Carro carro)
        {
            _context.Remove(carro);
            _context.SaveChanges();
        }

        public IList<Carro> GetAll()
        {
            return _context
                .Carro
                .ToList();
        }

        public Carro GetById(int id)
        {
            return _context
                .Carro
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Carro carro)
        {
            _context.Update(carro);
            _context.SaveChanges();
        }
    }
}
