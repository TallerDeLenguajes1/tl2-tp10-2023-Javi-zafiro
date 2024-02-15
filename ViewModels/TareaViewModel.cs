namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class TareaViewModel
{
    private int id;
    private int idTablero;
    private string nombre;
    private string descripcion;
    private string color;
    private EstadoTarea estado;
    private int? usuario_asignado;
    private List<usuario> lista;

    public int Id { get => id; set => id = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de la tarea")]
    public string Nombre { get => nombre; set => nombre = value; }

    [Display(Name = "Descripción de la tarea")]
    public string Descripcion { get => descripcion; set => descripcion = value; }
    
    [Display(Name = "Color de la tarea")]
    public string Color { get => color; set => color = value; }

    [Display(Name = "Usuario a asignar")]
    public int? Usuario_asignado { get => usuario_asignado; set => usuario_asignado = value; }
    public int IdTablero { get => idTablero; set => idTablero = value; }
    
    [Required(ErrorMessage = "No puede haber tarea sin estado")]
    [Display(Name = "Estado de la tarea")]
    public EstadoTarea Estado { get => estado; set => estado = value; }
    
    public List<usuario> Lista { get => lista; set => lista = value; }

    public TareaViewModel(tarea tarea, List<usuario> list){
        this.id = tarea.Id;
        this.idTablero = tarea.IdTablero;
        this.nombre = tarea.Nombre;
        this.descripcion = tarea.Descripcion;
        this.color = tarea.Color;
        this.estado = tarea.Estado;
        this.usuario_asignado = tarea.Usuario_asignado;
        this.Lista=list;
    }

    public TareaViewModel(tarea tarea){
        this.id = tarea.Id;
        this.idTablero = tarea.IdTablero;
        this.nombre = tarea.Nombre;
        this.descripcion = tarea.Descripcion;
        this.color = tarea.Color;
        this.estado = tarea.Estado;
        this.usuario_asignado = tarea.Usuario_asignado;
    }
    public TareaViewModel(){

    }
}