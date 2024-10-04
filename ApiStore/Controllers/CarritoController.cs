using ApiStore.Data;
using ApiStore.Models.Dto;
using ApiStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public CarritoController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "ObtenerTodosCarritos")]
        public async Task<IActionResult> ObtenerTodosCarritos()
        {
            try
            {
                var listacarritos = await _context.Carritos.ToListAsync();
                return Ok(listacarritos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerPorId/{IdCarrito:int}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute(Name = "IdCarrito")] int id)
        {
            try
            {
                var item = await _context.Carritos.FirstOrDefaultAsync(x => x.IdCarrito == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Crear([FromBody] Carrito carrito)
        {
            try
            {
                await _context.Carritos.AddAsync(carrito);
                var result = await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{IdCarrito:int}")]
        public async Task<IActionResult> Borrar([FromRoute] int IdCarrito)
        {
            try
            {
                var carritoExistente = await _context.Carritos.FindAsync(IdCarrito);

                if (carritoExistente != null)
                {
                    _context.Carritos.Remove(carritoExistente);
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
        public async Task<IActionResult> Modificar([FromBody] Carrito carrito, [FromRoute] int IdCarrito)
        {
            try
            {
                var carritoExistente = await _context.Carritos.FindAsync(IdCarrito);

                if (carritoExistente != null)
                {
                    if (carrito.FechaCreacion !=null) carritoExistente.FechaCreacion = carrito.FechaCreacion;
                    if (carrito.IdProductos != null) carritoExistente.IdProductos = carrito.IdProductos;
                    if (carrito.IdUsuario != null) carritoExistente.IdUsuario = carrito.IdUsuario;
                    if (carrito.Descripcion != null) carritoExistente.Descripcion = carrito.Descripcion;
       
                    _context.Carritos.Update(carritoExistente);
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
