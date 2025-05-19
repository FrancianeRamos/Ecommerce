using Ecommerce.Objects.DTOs;

namespace Ecommerce.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDTO>> ListarTodos();
        Task<PedidoDTO> ObterPorId(int id);
        Task<PedidoDTO> GerarPedido(PedidoDTO pedidoDTO);
        Task<PedidoDTO> Atualizar(PedidoDTO pedidoDTO, int id);

        Task<PedidoDTO> SucessoAoPagar(PedidoDTO pedidoDTO);
        Task<PedidoDTO> DespacharPedido(PedidoDTO pedidoDTO);
        Task<PedidoDTO> CancelarPedido(PedidoDTO pedidoDTO);
    }
}
