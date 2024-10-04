using ApiStore.Data;
using ApiStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoDetalleController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public CarritoDetalleController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "ObtenerDetalleCarritos")]
        public async Task<IActionResult> ObtenerDetalleCarritos()
        {
            try
            {
                var listadetallecarrito = await _context.DetalleCarritos.ToListAsync();
                return Ok(listadetallecarrito);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerPorId/{IdDetalleCarrito:int}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute(Name = "IdDetalleCarrito")] int id)
        {
            try
            {
                var item = await _context.DetalleCarritos.FirstOrDefaultAsync(x => x.IdDetalleCarrito == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Crear([FromBody] DetalleCarrito detallecarrito)
        {
            try
            {
                await _context.DetalleCarritos.AddAsync(detallecarrito);
                var result = await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{IdDetalleCarrito:int}")]
        public async Task<IActionResult> Borrar([FromRoute] int IdDetalleCarrito)
        {
            try
            {
                var carritodetalleExistente = await _context.DetalleCarritos.FindAsync(IdDetalleCarrito);

                if (carritodetalleExistente != null)
                {
                    _context.DetalleCarritos.Remove(carritodetalleExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{IdCarrito:int}")]
        public async Task<IActionResult> Modificar([FromBody] DetalleCarrito detallecarrito, [FromRoute] int IdDetalleCarrito)
        {
            try
            {
                var carritodetalleExistente = await _context.DetalleCarritos.FindAsync(IdDetalleCarrito);

                if (carritodetalleExistente != null)
                {
                    if (detallecarrito.FechaCreacionFactura != null) carritodetalleExistente.FechaCreacionFactura = detallecarrito.FechaCreacionFactura;
                    if (detallecarrito.FechaFactura != null) carritodetalleExistente.FechaFactura = detallecarrito.FechaFactura;
                    if (detallecarrito.IdCarrito != null) carritodetalleExistente.IdCarrito = detallecarrito.IdCarrito;
                    if (detallecarrito.PrecioTotalDetalleCarrito != null) carritodetalleExistente.PrecioTotalDetalleCarrito = detallecarrito.PrecioTotalDetalleCarrito;
                    if (detallecarrito.DetalleFactura != null) carritodetalleExistente.DetalleFactura = detallecarrito.DetalleFactura;

                    _context.DetalleCarritos.Update(carritodetalleExistente);
                    await _context.SaveChangesAsync();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

