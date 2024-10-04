using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStore.Models
{
    public class DetalleCarrito
    {
        public int IdDetalleCarrito { get; set; }
        public decimal PrecioTotalDetalleCarrito { get; set; }

        [ForeignKey("Carrito")]
        public int IdCarrito { get; set; }
        public DateTime FechaFactura { get; set; }
        public string? DetalleFactura { get; set; }
        public DateTime FechaCreacionFactura { get; set; }
    }
}
