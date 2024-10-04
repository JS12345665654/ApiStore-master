using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStore.Models.Dto
{
    public class ModificarCarritoDto
    {
        public int IdCarrito { get; set; }

        [ForeignKey("Productos")]
        public int? IdProductos { get; set; }

        [ForeignKey("Usuarios")]
        public int? IdUsuarios { get; set; }
        public string? Imagen { get; set; }
        public decimal PrecioTotalCarrito { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Descripcion { get; set; }
    }
}
