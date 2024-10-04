using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStore.Models;

public class Usuarios
{
    public int IdUsuario { get; set; }
    public string? Nombre { get; set; }
    public string? Contrasenia { get; set; }
    public string? Email { get; set; }
    public string? Imagen { get; set; }    
    public string? CategoriaPreferida {  get; set; }
    public int IdRol { get; set; }
    public bool Activo { get; set; }
}
