using Ecommerce.Objects.DTOs;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Services.Interfaces;


namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidosDTO = await _pedidoService.ListarTodos();
            return Ok(pedidosDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedidoDTO = await _pedidoService.ObterPorId(id);

            if (pedidoDTO is null)
            {
                return NotFound("Pedido não encontrado");
            }

            return Ok(pedidoDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PedidoDTO pedidoDTO)
        {
            if (pedidoDTO is null)
            {
                return BadRequest("Dados inválidos");
            }

            try
            {
                var pedidoCriado = await _pedidoService.GerarPedido(pedidoDTO);
                return Ok(pedidoCriado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao gerar pedido: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PedidoDTO pedidoDTO)
        {
            if (pedidoDTO is null)
            {
                return BadRequest("Dados inválidos");
            }

            try
            {
                var existingPedidoDTO = await _pedidoService.ObterPorId(id);
                if (existingPedidoDTO is null)
                {
                    return NotFound("O pedido informado não existe");
                }

                var pedidoAtualizado = await _pedidoService.Atualizar(pedidoDTO, id);

                return Ok(pedidoAtualizado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar pedido: {ex.Message}");
            }
        }

        [HttpPut("SucessoAoPagar/{id}")]
        public async Task<IActionResult> SucessoAoPagar(int id)
        {
            try
            {
                var existingPedidoDTO = await _pedidoService.ObterPorId(id);
                if (existingPedidoDTO is null)
                {
                    return NotFound("O pedido informado não existe");
                }

                var pedidoDTO = await _pedidoService.SucessoAoPagar(existingPedidoDTO);

                return Ok(pedidoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao confirmar pagamento: {ex.Message}");
            }
        }

        [HttpPut("Despachar/{id}")]
        public async Task<IActionResult> DespacharPedido(int id)
        {
            try
            {
                var existingPedidoDTO = await _pedidoService.ObterPorId(id);
                if (existingPedidoDTO is null)
                {
                    return NotFound("O pedido informado não existe");
                }

                var pedidoDTO = await _pedidoService.DespacharPedido(existingPedidoDTO);

                return Ok(pedidoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao despachar pedido: {ex.Message}");
            }
        }

        [HttpPut("Cancelar/{id}")]
        public async Task<IActionResult> CancelarPedido(int id)
        {
            try
            {
                var existingPedidoDTO = await _pedidoService.ObterPorId(id);
                if (existingPedidoDTO is null)
                {
                    return NotFound("O pedido informado não existe");
                }

                var pedidoDTO = await _pedidoService.CancelarPedido(existingPedidoDTO);

                return Ok(pedidoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cancelar pedido: {ex.Message}");
            }
        }
    }
}
