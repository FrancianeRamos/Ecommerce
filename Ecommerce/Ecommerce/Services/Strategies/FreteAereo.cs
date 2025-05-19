namespace Ecommerce.Services.Strategies
{
    public class FreteAereo : IFrete
    {
        public double CalcularFrete(double valorPedido)
        {
            return valorPedido * 0.1;
        }
    }
}
