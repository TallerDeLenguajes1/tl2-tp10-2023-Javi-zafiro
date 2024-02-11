namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class ListaTablerosViewModel
{
    private List<TableroViewModel> listaTabPropios;
    private List<TableroViewModel> listaTabNoPropios;
    private List<TableroViewModel> listaTabTodos;
    private bool admin;


    public List<TableroViewModel> ListaTabPropios { get => listaTabPropios; set => listaTabPropios = value; }
    public List<TableroViewModel> ListaTabNoPropios { get => listaTabNoPropios; set => listaTabNoPropios = value; }
    public List<TableroViewModel> ListaTabTodos { get => listaTabTodos; set => listaTabTodos = value; }
    public bool Admin { get => admin; set => admin = value; }

    public ListaTablerosViewModel(List<tablero> tablerosPropios, List<tablero> tablerosNoPropios)
    {
        this.ListaTabPropios=new List<TableroViewModel>();
        foreach (var item in tablerosPropios)
        {
            ListaTabPropios.Add(new TableroViewModel(item));
        }

        this.ListaTabNoPropios=new List<TableroViewModel>();
        foreach (var item in tablerosNoPropios)
        {
            ListaTabNoPropios.Add(new TableroViewModel(item));
        }
        this.Admin=false;
    }
    public ListaTablerosViewModel(List<tablero> tableros)
    {
        this.ListaTabTodos=new List<TableroViewModel>();
        foreach (var item in tableros)
        {
            ListaTabTodos.Add(new TableroViewModel(item));
        }
        this.Admin=true;
    }
    public ListaTablerosViewModel()
    {
    }
    
}