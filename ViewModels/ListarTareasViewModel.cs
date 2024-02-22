namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class ListarTareasViewModel
{
    private List<TareaViewModel> listaTar;
    private bool admin;
    private int? idPropietario;
    private int? idLogeado;
    private int? idTablero;
    private string? tablero;
    
    public ListarTareasViewModel(List<TareaViewModel> tareas, bool ad)
    {
        this.listaTar=tareas; //new List<TareaViewModel>();
        /*
        foreach (var item in tareas)
        {
            listaTar.Add(new TareaViewModel(item));
        }
        this.admin=ad;*/
    }
    public ListarTareasViewModel(List<TareaViewModel> tareas, bool ad, int usu, int usuLog, string tab, int idTab)
    {
        this.listaTar= tareas;//new List<TareaViewModel>();
        /*
        foreach (var item in tareas)
        {
            listaTar.Add(new TareaViewModel(item));
        }*/
        this.admin=ad;
        this.IdPropietario=usu;
        this.idLogeado=usuLog;
        this.tablero=tab;
        this.idTablero=idTab;
    }
    public ListarTareasViewModel()
    {
    }

    public List<TareaViewModel> ListaTar { get => listaTar; set => listaTar = value; }
    public bool Admin { get => admin; set => admin = value; }
    public int? IdPropietario { get => idPropietario; set => idPropietario = value; }
    public int? IdLogeado { get => idLogeado; set => idLogeado = value; }
    public int? IdTablero { get => idTablero; set => idTablero = value; }
    public string? Tablero { get => tablero; set => tablero = value; }
}