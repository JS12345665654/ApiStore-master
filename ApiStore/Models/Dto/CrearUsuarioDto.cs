namespace ApiStore.Models.Dto
{
    public class CrearUsuarioDto
    {
        public string? Nombre { get; set; }
        public string? Contrasenia { get; set; }
        public string? Email { get; set; }
        public IFormFile Imagen { get; set; }
        public string? CategoriaPreferida { get; set; }
        public int IdRol { get; set; }
        public bool Activo { get; set; }
    }
}
