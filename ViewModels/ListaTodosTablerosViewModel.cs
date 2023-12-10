namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class ListaTodosTablerosViewModel
{
    private List<TableroViewModel> listaTab;

    public ListaTodosTablerosViewModel(List<tablero> tableros)
    {
        this.listaTab=new List<TableroViewModel>();
        foreach (var item in tableros)
        {
            ListaTab.Add(new TableroViewModel(item));
        }
    }

    public List<TableroViewModel> ListaTab { get => listaTab; set => listaTab = value; }
    public ListaTodosTablerosViewModel()
    {
    }
    
}