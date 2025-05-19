namespace Ecommerce.Services.State
{
    public interface IPedidoState
    {
        IPedidoState SucessoAoPagar();
        IPedidoState DespacharPedido();
        IPedidoState CancelarPedido();
    }
}
