using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSP.Data;
using SSP.Models;
using SSP.ViewModels;
using SSP.Functions;

[Authorize(Policy = "AdminPolicy")]
public class EncriptarController : Controller
{
    private readonly ClsEncrypt _encrypt;

    public EncriptarController(IConfiguration config)
    {
        _encrypt = new ClsEncrypt(config);
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    // Encripta un texto
    [HttpPost]
    public IActionResult Encriptar(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            return Json(new { success = false, mensaje = "Por favor ingrese un texto." });
        }

        try
        {
            // Se invoca el método que utiliza AES-CBC con salt y lo convierte a Base64
            string sEncriptado = _encrypt.FnsEncripta(texto)?.SEncript ?? "";
            return Json(new { success = true, resultado = sEncriptado });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, mensaje = "Error al encriptar: " + ex.Message });
        }
    }
}