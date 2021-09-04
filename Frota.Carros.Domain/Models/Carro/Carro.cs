namespace Frota.Carros.Domain.Models.Carro
{
    public class Carro
    {
        public int Id { get; private set; }
        public string Placa { get; private set; }
        public string Marca { get; private set; }
        public string AnoFabricacao { get; private set; }

        public Carro()
        {

        }

        public Carro(string placa, string marca, string anoFabricacao)
        {
            Placa = placa;
            Marca = marca;
            AnoFabricacao = anoFabricacao;
        }

        public void AlterarPlaca(string placa)
        {
            Placa = placa;
        }

        public void AlterarMarca(string marca)
        {
            Marca = marca;
        }

        public void AlterarAnoFabricacao(string anoFabricacao)
        {
            AnoFabricacao = anoFabricacao;
        }
    }
}
