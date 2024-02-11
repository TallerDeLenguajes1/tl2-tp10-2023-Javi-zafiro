namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class AsignarUsuarioViewModel
{
    private int id;
    private string nombre;
    private string descripcion;
    private int? usuario_asignado;
    private List<usuario> lista;
    private int? idTablero;

    public int Id { get => id; set => id = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de la tarea")]
    public string Nombre { get => nombre; set => nombre = value; }

    [Display(Name = "Descripción de la tarea")]
    public string Descripcion { get => descripcion; set => descripcion = value; }

    [Display(Name = "Usuario a asignar")]
    public int? Usuario_asignado { get => usuario_asignado; set => usuario_asignado = value; }
    public List<usuario> Lista { get => lista; set => lista = value; }
    public int? IdTablero { get => idTablero; set => idTablero = value; }

    public AsignarUsuarioViewModel(tarea tarea, List<usuario> list){
        this.id = tarea.Id;
        this.nombre = tarea.Nombre;
        this.descripcion = tarea.Descripcion;
        this.usuario_asignado = tarea.Usuario_asignado;
        this.lista=list;
        this.idTablero=tarea.IdTablero;
    }
    public AsignarUsuarioViewModel(List<usuario> list){
        this.lista=list;
    }
    public AsignarUsuarioViewModel(){

    }
}