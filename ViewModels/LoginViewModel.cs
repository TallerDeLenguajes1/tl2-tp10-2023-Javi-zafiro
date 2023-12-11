namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    private string nombre;
    private string contrasenia;

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de usuario")]
    public string Nombre { get => nombre; set => nombre = value; }
    
    [Display(Name = "Contraseña")]
    [MinLength(4, ErrorMessage = "Como minimo 4 caracteres")]
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    
    public LoginViewModel(login usu)
    {
        this.Nombre=usu.Nombre;
        this.Contrasenia=usu.Contrasenia;
    }
    public LoginViewModel()
    {
    }
}