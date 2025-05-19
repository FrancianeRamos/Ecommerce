namespace Ecommerce.Services.State
{
    public class CanceladoState : IPedidoState
    {
        public IPedidoState CancelarPedido()
        {
            throw new Exception("Operação não suportada, o pedido foi cancelado");
        }

        public IPedidoState DespacharPedido()
        {
            throw new Exception("Operação não suportada, o pedido foi cancelado");
        }

        public IPedidoState SucessoAoPagar()
        {
            throw new Exception("Operação não suportada, o pedido foi cancelado");
        }
    }
}
