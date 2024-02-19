namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class ListaTablerosViewModel
{
    private List<TableroListaViewModel> listaTabPropios;
    private List<TableroListaViewModel> listaTabNoPropios;
    private List<TableroListaViewModel> listaTabTodos;
    private bool admin;


    public List<TableroListaViewModel> ListaTabPropios { get => listaTabPropios; set => listaTabPropios = value; }
    public List<TableroListaViewModel> ListaTabNoPropios { get => listaTabNoPropios; set => listaTabNoPropios = value; }
    public List<TableroListaViewModel> ListaTabTodos { get => listaTabTodos; set => listaTabTodos = value; }
    public bool Admin { get => admin; set => admin = value; }

    public ListaTablerosViewModel(List<tablero> tablerosPropios, List<tablero> tablerosNoPropios)
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
        this.Admin=false;
    }
    public ListaTablerosViewModel(List<tablero> tableros, bool ad)
    {
        this.ListaTabTodos=new List<TableroListaViewModel>();
        foreach (var item in tableros)
        {
            ListaTabTodos.Add(new TableroListaViewModel(item));
        }
        this.Admin=ad;
    }
    public ListaTablerosViewModel(List<tablero> tableros)
    {
        this.ListaTabPropios=new List<TableroListaViewModel>();
        this.ListaTabNoPropios=new List<TableroListaViewModel>();
        foreach (var item in tableros)
        {
            ListaTabPropios.Add(new TableroListaViewModel(item));
        }
        this.Admin=false;
    }
    public ListaTablerosViewModel()
    {
    }
    
}