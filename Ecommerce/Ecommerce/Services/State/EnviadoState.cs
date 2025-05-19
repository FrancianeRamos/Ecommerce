namespace Ecommerce.Services.State
{
    public class EnviadoState : IPedidoState
    {
        public IPedidoState CancelarPedido()
        {
            throw new Exception("Operação não suportada, o pedido já foi enviado");
        }

        public IPedidoState DespacharPedido()
        {
            throw new Exception("Operação não suportada, o pedido já foi enviado");
        }

        public IPedidoState SucessoAoPagar()
        {
            throw new Exception("Operação não suportada, o pedido já foi enviado");
        }
    }
}
