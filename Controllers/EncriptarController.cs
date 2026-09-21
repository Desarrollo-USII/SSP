using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity; // Necesario para IPasswordHasher
using SSP.Models;
using System;

[Authorize(Policy = "AdminPolicy")]
public class EncriptarController : Controller
{
    private readonly IPasswordHasher<MoUsuario> _passwordHasher;

    // Inyectamos el IPasswordHasher en lugar de ClsEncrypt
    public EncriptarController(IPasswordHasher<MoUsuario> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    // Genera el Hash de una contraseña
    [HttpPost]
    public IActionResult Encriptar(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            return Json(new { success = false, mensaje = "Por favor ingrese un texto o contraseña." });
        }

        try
        {
            // El IPasswordHasher nativo requiere una instancia del modelo, 
            // pasamos una nueva instancia vacía de MoUsuario (el algoritmo por defecto solo usa la cadena de texto y genera su propia sal).
            string sHashGenerado = _passwordHasher.HashPassword(new MoUsuario(), texto);
            
            return Json(new { success = true, resultado = sHashGenerado });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, mensaje = "Error al generar el Hash: " + ex.Message });
        }
    }
}