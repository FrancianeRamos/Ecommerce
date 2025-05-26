using Ecommerce.Data.Interfaces;
using Ecommerce.Models;
using Ecommerce.Objects.DTOs;
using Ecommerce.Objects.Enums;
using Ecommerce.Services.Interfaces;
using Ecommerce.Services.State;
using Ecommerce.Services.Strategies;

namespace Ecommerce.Services.Entities
{
    public class PedidoService : Pedido, IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _repository = pedidoRepository;
        }

        public async Task<IEnumerable<PedidoDTO>> ListarTodos()
        {
            var pedidos = await _repository.Get();
            List<PedidoDTO> pedidosDTO = new List<PedidoDTO>();

            foreach (var pedido in pedidos)
            {
                var pedidoConvertido = ConverterDeModelsParaEntities(pedido);
                pedidosDTO.Add(ConverterParaDTO(pedidoConvertido));
            }

            return pedidosDTO;
        }
      
        private Ecommerce.Models.Pedido ConverterDeModelsParaEntities(Ecommerce.Models.Pedido pedido)
        {
            return new Ecommerce.Models.Pedido
            {
                Id = pedido.Id,
                SubTotal = pedido.SubTotal,
                ValorFrete = pedido.ValorFrete,
                EstadoAtual = pedido.EstadoAtual,
                TipoFrete = pedido.TipoFrete,
            };
        }

        public async Task<PedidoDTO> ObterPorId(int id)
        {
            var pedido = await _repository.GetById(id);

            if (pedido is null)
            {
                return null;
            }

            // Fix: Convert the "Ecommerce.Models.Pedido" to "Ecommerce.Services.Entities.Pedido"
            var pedidoConvertido = ConverterDeModelsParaEntities(pedido);

            return ConverterParaDTO(pedidoConvertido);
        }

        public async Task<PedidoDTO> GerarPedido(PedidoDTO pedidoDTO)
        {
            var pedido = ConverterParaModel(pedidoDTO);
            IFrete frete = CriarFretePorTipo(pedido.TipoFrete);

            pedido.EstadoAtual = EstadoPedido.Pendente;
            pedido.ValorFrete = frete.CalcularFrete(pedido.SubTotal);

            await _repository.Add(pedido);
            return ConverterParaDTO(pedido);
        }

        public async Task<PedidoDTO> Atualizar(PedidoDTO pedidoDTO, int id)
        {
            var existingPedido = await _repository.GetById(id);

            if (existingPedido is null)
            {
                throw new KeyNotFoundException($"Pedido com id {id} não encontrado.");
            }

            if (existingPedido.EstadoAtual == EstadoPedido.Pendente)
            {

                pedidoDTO.EstadoAtual = (int)existingPedido.EstadoAtual;

                IFrete frete = CriarFretePorTipo((TipoFrete)pedidoDTO.TipoFrete);
                pedidoDTO.ValorFrete = frete.CalcularFrete(pedidoDTO.SubTotal);
            }
            else
            {
                throw new Exception("Não é permitido atualizar o pedido, após sua confirmação/cancelamento.");
            }

            var pedido = ConverterParaModel(pedidoDTO);
            await _repository.Update(pedido);

            return pedidoDTO;
        }

        public async Task<PedidoDTO> SucessoAoPagar(PedidoDTO pedidoDTO)
        {

            IPedidoState state = ObterEstadoClasse(ConverterParaModel(pedidoDTO).EstadoAtual);

            IPedidoState novoEstado = state.SucessoAoPagar();

            pedidoDTO.EstadoAtual = (int)ObterEstadoEnum(novoEstado);

            await _repository.Update(ConverterParaModel(pedidoDTO));

            return pedidoDTO;
        }

        public async Task<PedidoDTO> DespacharPedido(PedidoDTO pedidoDTO)
        {
            IPedidoState state = ObterEstadoClasse(ConverterParaModel(pedidoDTO).EstadoAtual);
            IPedidoState novoEstado = state.DespacharPedido();
            pedidoDTO.EstadoAtual = (int)ObterEstadoEnum(novoEstado);

            await _repository.Update(ConverterParaModel(pedidoDTO));

            return pedidoDTO;
        }

        public async Task<PedidoDTO> CancelarPedido(PedidoDTO pedidoDTO)
        {
            IPedidoState state = ObterEstadoClasse(ConverterParaModel(pedidoDTO).EstadoAtual);
            IPedidoState novoEstado = state.CancelarPedido();
            pedidoDTO.EstadoAtual = (int)ObterEstadoEnum(novoEstado);

            await _repository.Update(ConverterParaModel(pedidoDTO));

            return pedidoDTO;
        }

  
        private IPedidoState ObterEstadoClasse(EstadoPedido estadoPedido)
        {
            return estadoPedido switch
            {
                EstadoPedido.Pendente => new AguardandoPagamentoState(),
                EstadoPedido.Aprovado => new PagoState(),
                EstadoPedido.Cancelado => new CanceladoState(),
                EstadoPedido.Processando => new EnviadoState(),
                _ => throw new ArgumentException("Estado inválido"),
            };
        }

        private EstadoPedido ObterEstadoEnum(IPedidoState state)
        {
            return state switch
            {
                AguardandoPagamentoState _ => EstadoPedido.Pendente,
                PagoState _ => EstadoPedido.Aprovado,
                EnviadoState _ => EstadoPedido.Processando,
                CanceladoState _ => EstadoPedido.Cancelado,
                _ => throw new ArgumentException("Estado inválido"),
            };
        }

        private IFrete CriarFretePorTipo(TipoFrete tipoFrete)
        {
            return tipoFrete switch
            {
                TipoFrete.aereo => new FreteAereo(),
                TipoFrete.terrestre => new FreteTerrestre(),
                _ => throw new ArgumentException("Tipo de frete inválido"),
            };
        }

        private PedidoDTO ConverterParaDTO(Pedido pedido)
        {
            PedidoDTO pedidoDTO = new()
            {
                Id = pedido.Id,
                SubTotal = pedido.SubTotal,
                ValorFrete = pedido.ValorFrete,
                EstadoAtual = (int)pedido.EstadoAtual,
                TipoFrete = (int)pedido.TipoFrete,
            };

            return pedidoDTO;
        }

        private Pedido ConverterParaModel(PedidoDTO pedidoDTO)
        {
            Pedido pedido = new()
            {
                Id = pedidoDTO.Id,
                SubTotal = pedidoDTO.SubTotal,
                ValorFrete = pedidoDTO.ValorFrete,
                EstadoAtual = (EstadoPedido)pedidoDTO.EstadoAtual,
                TipoFrete = (TipoFrete)pedidoDTO.TipoFrete,
            };

            return pedido;
        }

    }
}
