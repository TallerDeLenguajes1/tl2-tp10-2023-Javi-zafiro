namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class ListaTodosTablerosViewModel
{
    private List<TableroListaViewModel> listaTabPropios;
    private List<TableroListaViewModel> listaTabNoPropios;



    public List<TableroListaViewModel> ListaTabPropios { get => listaTabPropios; set => listaTabPropios = value; }
    public List<TableroListaViewModel> ListaTabNoPropios { get => listaTabNoPropios; set => listaTabNoPropios = value; }


    public ListaTodosTablerosViewModel(List<tablero> tablerosPropios, List<tablero> tablerosNoPropios)
    {
        this.ListaTabPropios=new List<TableroListaViewModel>();
        foreach (var item in tablerosPropios)
        {
            ListaTabPropios.Add(new TableroListaViewModel(item));
        }

        this.ListaTabNoPropios=new List<TableroListaViewModel>();
        foreach (var item in tablerosNoPropios)
        {
            ListaTabNoPropios.Add(new TableroListaViewModel(item));
        }
    }
    public ListaTodosTablerosViewModel(List<tablero> tableros)
    {
        this.ListaTabPropios=new List<TableroListaViewModel>();
        this.ListaTabNoPropios=new List<TableroListaViewModel>();
        foreach (var item in tableros)
        {
            ListaTabPropios.Add(new TableroListaViewModel(item));
        }
    }
    public ListaTodosTablerosViewModel()
    {
    }
    
}