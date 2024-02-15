namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class ListarTareasViewModel
{
    private List<TareaViewModel> listaTar;
    private bool admin;
    private int? idPropietario;
    private int? idLogeado;
    private string? tablero;
    
    public ListarTareasViewModel(List<tarea> tareas, bool ad)
    {
        this.listaTar= new List<TareaViewModel>();
        foreach (var item in tareas)
        {
            listaTar.Add(new TareaViewModel(item));
        }
        this.admin=ad;
    }
    public ListarTareasViewModel(List<tarea> tareas, bool ad, int usu, int usuLog, string tab)
    {
        this.listaTar= new List<TareaViewModel>();
        foreach (var item in tareas)
        {
            listaTar.Add(new TareaViewModel(item));
        }
        this.admin=ad;
        this.IdPropietario=usu;
        this.idLogeado=usuLog;
        this.tablero=tab;
    }
    public ListarTareasViewModel()
    {
    }

    public List<TareaViewModel> ListaTar { get => listaTar; set => listaTar = value; }
    public bool Admin { get => admin; set => admin = value; }
    public int? IdPropietario { get => idPropietario; set => idPropietario = value; }
    public int? IdLogeado { get => idLogeado; set => idLogeado = value; }
    public string? Tablero { get => tablero; set => tablero = value; }
}