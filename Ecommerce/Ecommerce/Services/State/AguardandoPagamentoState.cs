namespace Ecommerce.Services.State
{
    public class AguardandoPagamentoState : IPedidoState
    {
        public IPedidoState CancelarPedido()
        {
            return new CanceladoState();
        }

        public IPedidoState DespacharPedido()
        {
            throw new Exception("Operação não suportada, " +
                "o pedido ainda não foi pago");
        }

        public IPedidoState SucessoAoPagar()
        {
            return new PagoState();
        }
    }
}
