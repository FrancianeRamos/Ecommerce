using Ecommerce.Objects.Enums;

namespace Ecommerce.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public double SubTotal { get; set; }
        public double ValorFrete { get; set; } 
        public EstadoPedido EstadoAtual { get; set; }
        public TipoFrete TipoFrete { get; set; }

        public Pedido (){}

        public Pedido(double subTotal, double valorFrete, EstadoPedido estadoAtual, TipoFrete tipoFrete)
        {
            SubTotal = subTotal;
            ValorFrete = valorFrete;
            EstadoAtual = estadoAtual;
            TipoFrete = tipoFrete;
        }
    }
}
