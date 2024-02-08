namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class CrearTableroViewModel
{
        private int id;
    private int idUsuariPropietario;
    private string? nombre;
    private string? descripcion;

    public int Id { get => id; set => id = value; }
    
    [Required(ErrorMessage = "No puede existir un tablero sin propietario")]
    [Display(Name = "Usuario Propietario")]
    public int IdUsuariPropietario { get => idUsuariPropietario; set => idUsuariPropietario = value; }
    
    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre del tablero")]
    public string? Nombre { get => nombre; set => nombre = value; }
    
    [Display(Name = "Descripción del tablero")]
    public string? Descripcion { get => descripcion; set => descripcion = value; }

    public CrearTableroViewModel(tablero tablero){
        this.id = tablero.Id;
        this.nombre = tablero.Nombre;
        this.descripcion = tablero.Descripcion;
        this.idUsuariPropietario = tablero.IdUsuariPropietario;
    }

    public CrearTableroViewModel(int idPropietario){
        this.IdUsuariPropietario=idPropietario;
    }

    public CrearTableroViewModel()
    {
    }
}