namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class ListarTareasViewModel
{
    private List<TareaViewModel> listaTar;

    public ListarTareasViewModel(List<tarea> tareas)
    {
        this.listaTar= new List<TareaViewModel>();
        foreach (var item in tareas)
        {
            listaTar.Add(new TareaViewModel(item));
        }
    }
    public ListarTareasViewModel()
    {
    }

    public List<TareaViewModel> ListaTar { get => listaTar; set => listaTar = value; }
}