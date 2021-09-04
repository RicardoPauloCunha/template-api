using System.Collections.Generic;

namespace Frota.Carros.Domain.Models.Carro
{
    public interface ICarroRepository
    {
        Carro GetById(int id);
        IList<Carro> GetAll();
        void Create(Carro carro);
        void Delete(Carro carro);
        void Update(Carro carro);
    }
}
