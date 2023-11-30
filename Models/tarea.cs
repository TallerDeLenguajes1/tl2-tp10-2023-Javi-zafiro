namespace tl2_tp10_2023_Javi_zafiro.Models;
 

 public enum EstadoTarea
 {
    Ideas=1,
    ToDo=2,
    Doing=3,
    Review=4,
    Done=5
 }
public class tarea
{
    private int id;
    private int idTablero;
    private string nombre;
    private string descripcion;
    private string color;
    private EstadoTarea estado;
    private int? usuario_asignado;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public int? Usuario_asignado { get => usuario_asignado; set => usuario_asignado = value; }
    public int IdTablero { get => idTablero; set => idTablero = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
}