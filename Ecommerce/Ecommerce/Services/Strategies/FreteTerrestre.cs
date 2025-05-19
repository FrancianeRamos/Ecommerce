namespace Ecommerce.Services.Strategies
{
    public class FreteTerrestre : IFrete
    {
        public double CalcularFrete(double valorPedido)
        {
            return valorPedido * 0.05;
        }
    }
}
