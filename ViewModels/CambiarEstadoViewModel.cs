namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class CambiarEstadoViewModel
{
    private int id;
    private string nombre;
    private string descripcion;
    private EstadoTarea estado;
    private int? idTablero;

    public int Id { get => id; set => id = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de la tarea")]
    public string Nombre { get => nombre; set => nombre = value; }

    [Display(Name = "Descripción de la tarea")]
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public int? IdTablero { get => idTablero; set => idTablero = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }

    public CambiarEstadoViewModel(tarea tarea){
        this.id = tarea.Id;
        this.nombre = tarea.Nombre;
        this.descripcion = tarea.Descripcion;
        this.idTablero=tarea.IdTablero;
        this.estado=tarea.Estado;
    }
    public CambiarEstadoViewModel(){

    }
}