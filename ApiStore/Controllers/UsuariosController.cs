using ApiStore.Data;
using ApiStore.Models;
using ApiStore.Models.Dto;
using ApiStore.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public UsuariosController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpPost("ValidarCredencial")]
        public async Task<IActionResult> ValidarCredencial([FromBody] UsuarioLoginDto usuario)
        {
            var existeLogin = await _context.Usuarios
                .AnyAsync(x => x.Email.Equals(usuario.Email) && x.Contrasenia.Equals(usuario.Contrasenia));

            Usuarios usuarioLogin = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email.Equals(usuario.Email) && x.Contrasenia.Equals(usuario.Contrasenia));

            
            if (!existeLogin)
            {
                return NotFound("Usuario No Existe");
            }

            LoginResponseDto loginReponse = new LoginResponseDto()
            {
                Autenticado = existeLogin,
                Email = existeLogin ? usuarioLogin.Email : "",
                Nombre = existeLogin ? usuarioLogin.Nombre : "",
                IdRol = existeLogin ? usuarioLogin.IdRol : 0,
                IdUsuario = existeLogin ? usuarioLogin.IdUsuario : 0
            };  

            return Ok(loginReponse);
        }

        [HttpGet(Name = "ObtenerTodosUsuarios")]
        public async Task<IActionResult> ObtenerTodosUsuarios()
        {
            try
            {
                var listausuarios = await _context.Usuarios.ToListAsync();
                return Ok(listausuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerPorId/{IdUsuario:int}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute(Name = "IdUsuario")] int id)
        {
            try
            {
                var item = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Crear([FromBody] Usuarios usuarios)
        {
            try
            {
                usuarios.Imagen = "Imagen/";
                await _context.Usuarios.AddAsync(usuarios);
                var result = await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CrearConImagen")]
        public async Task<IActionResult> CrearConImagen([FromForm] CrearUsuarioDto crearUsuarios)
        {
            try
            {

                var usuarios = new Usuarios()
                {
                    Nombre = crearUsuarios.Nombre,
                    Email = crearUsuarios.Email,
                    Contrasenia = crearUsuarios.Contrasenia,
                    CategoriaPreferida = crearUsuarios.CategoriaPreferida,
                    IdRol = crearUsuarios.IdRol,
                    Activo = crearUsuarios.Activo
                };

                // Subida Archivo
                if (crearUsuarios.Imagen != null)
                {
                    string nombreArchivo = usuarios.IdUsuario + Guid.NewGuid().ToString() + Path.GetExtension(crearUsuarios.Imagen.FileName);
                    string rutaArchivo = @"wwwroot\ImagenesProductos\" + nombreArchivo;

                    var ubicacionDirectorio = Path.Combine(Directory.GetCurrentDirectory(), rutaArchivo);

                    FileInfo file = new FileInfo(ubicacionDirectorio);

                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    using (var fileStream = new FileStream(ubicacionDirectorio, FileMode.Create))
                    {
                        crearUsuarios.Imagen.CopyTo(fileStream);
                    }

                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    // ubicacion acceso exterior desde navegador
                    usuarios.Imagen = baseUrl + "/ImagenesProductos/" + nombreArchivo;
                    // ubicacion local en servidor
                    usuarios.Imagen = rutaArchivo;
                }
                else
                {
                    usuarios.Imagen = "https://placehold.com/600x400";
                }
                await _context.Usuarios.AddAsync(usuarios);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{IdUsuario:int}")]
        public async Task<IActionResult> Borrar([FromRoute] int IdUsuario)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(IdUsuario);

                if (usuarioExistente != null)
                {
                    _context.Usuarios.Remove(usuarioExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{IdUsuario:int}")]
        public async Task<IActionResult> Modificar([FromBody] Usuarios usuarios, [FromRoute] int IdUsuario)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(IdUsuario);

                if (usuarioExistente != null)
                {
                    if (!usuarios.Email.IsNullOrEmpty()) usuarioExistente.Email = usuarios.Email;
                    if (!usuarios.Nombre.IsNullOrEmpty()) usuarioExistente.Nombre = usuarios.Nombre;
                    if (!usuarios.Contrasenia.IsNullOrEmpty()) usuarioExistente.Contrasenia = usuarios.Contrasenia;
                    if (!usuarios.CategoriaPreferida.IsNullOrEmpty()) usuarioExistente.CategoriaPreferida = usuarios.CategoriaPreferida;
                    if (usuarios.IdRol != null) usuarioExistente.IdRol = usuarios.IdRol;
                    if (usuarioExistente.Activo != null) usuarioExistente.Activo = usuarios.Activo;

                    _context.Usuarios.Update(usuarioExistente);
                    await _context.SaveChangesAsync();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GuardarImagen")]
        public async Task<IActionResult> GuardarImagen([FromForm] UploadFileApi archivo)
        {
            var ruta = string.Empty;

            if (archivo.Archivo.Length > 0)
            {
                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.Archivo.FileName);
                ruta = $"Images/{nombreArchivo}";
                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    try
                    {
                        await archivo.Archivo.CopyToAsync(stream);
                        // TODO: grabar ruta archivo en BD                    
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("Error al grabar archivo: " + ex.Message);
                    }
                }
            }
            return Ok();
        }
    }
}
