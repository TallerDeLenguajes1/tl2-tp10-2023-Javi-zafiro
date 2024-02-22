namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class UsuarioViewModel
{
    private int id;
    private string nombreDeUsuario;
    private TiposUsuario tipo;
    private string contrasenia;

    public int Id { get => id; set => id = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de usuario")]
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    
    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Tipo de usuario")]
    public TiposUsuario Tipo { get => tipo; set => tipo = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Contraseña")]
    [MinLength(4, ErrorMessage = "Como minimo 4 caracteres")]
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }

    public UsuarioViewModel(usuario usu){
        this.Id = usu.Id;
        this.NombreDeUsuario=usu.NombreDeUsuario;
        this.Tipo=usu.Tipo;
    }

    public UsuarioViewModel(){
        
    }
}