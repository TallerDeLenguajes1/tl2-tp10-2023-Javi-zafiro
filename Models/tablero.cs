namespace tl2_tp10_2023_Javi_zafiro.Models;
using tl2_tp10_2023_Javi_zafiro.ViewModels;

public class tablero
{
    private int id;
    private int idUsuariPropietario;
    private string nombre;
    private string descripcion;

    public int Id { get => id; set => id = value; }
    public int IdUsuariPropietario { get => idUsuariPropietario; set => idUsuariPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

    public tablero(TableroViewModel tablero){
        this.id = tablero.Id;
        this.nombre = tablero.Nombre;
        this.descripcion = tablero.Descripcion;
        this.idUsuariPropietario = tablero.IdUsuariPropietario;
    }

    public tablero(CrearTableroViewModel tablero){
        this.id = tablero.Id;
        this.nombre = tablero.Nombre;
        this.descripcion = tablero.Descripcion;
        this.idUsuariPropietario = tablero.IdUsuariPropietario;
    }
    public tablero(){
        
    }
}