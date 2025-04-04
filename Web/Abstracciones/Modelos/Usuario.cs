using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class Usuario
    {
        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "El usuario es requerido")]
        [StringLength(20, ErrorMessage = "El tamaño debe ser de entre 5 y 20", MinimumLength = 5)]
        [Display(Name = "Nombre Usuario")]
        public string? NombreUsuario { get; set; }
        [BindProperty(SupportsGet = true)]

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, ErrorMessage = "El tamaño debe ser de entre 5 y 10", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name ="Contraseña")]
        public string? Contrasenia { get; set; }
        [BindProperty(SupportsGet = true)]

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, ErrorMessage = "El tamaño debe ser de entre 5 y 10", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Contrasenia")]
        public string? ConfirmarContrasenia { get; set; }
        public string? PasswordHash { get; set; }        
        public Guid? IdServicio { get; set; }
        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [StringLength(100, ErrorMessage = "El tamaño debe ser de entre 5 y 10", MinimumLength = 5)]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string? CorreoElectronico { get; set; }
    }
}
