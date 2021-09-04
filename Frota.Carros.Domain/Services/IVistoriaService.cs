using System.Threading.Tasks;

namespace Frota.Carros.Domain.Services
{
    public interface IVistoriaService
    {
        Task AgendarVistoriaCarro(int carroId);
    }
}
