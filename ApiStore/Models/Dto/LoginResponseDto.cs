﻿namespace ApiStore.Models.Dto;

public class LoginResponseDto
{
    public int IdUsuario { get; set; }
    public string Nombre { get; set; }
    public int IdRol { get; set; }
    public bool Autenticado { get; set; }
    public string Email { get; set; }
}
