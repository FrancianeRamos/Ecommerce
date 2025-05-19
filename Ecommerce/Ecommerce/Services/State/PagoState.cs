namespace Ecommerce.Services.State
{
    public class PagoState : IPedidoState
    {
        public IPedidoState CancelarPedido()
        {
            return new CanceladoState();
        }

        public IPedidoState DespacharPedido()
        {
            return new EnviadoState();
        }

        public IPedidoState SucessoAoPagar()
        {
            throw new Exception("Operação não suportada, o pedido já foi pago");
        }
    }
}
