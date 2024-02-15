namespace tl2_tp10_2023_Javi_zafiro.ViewModels;
using tl2_tp10_2023_Javi_zafiro.Models;
using System.ComponentModel.DataAnnotations;

public class ListaUsuariosViewModel
{
    private List<UsuarioViewModel> listaUsuariosVM;

    public List<UsuarioViewModel> ListaUsuariosVM { get => listaUsuariosVM; set => listaUsuariosVM = value; }

    public ListaUsuariosViewModel(List<usuario> usuarios)
    {
        this.ListaUsuariosVM= new List<UsuarioViewModel>();
        foreach (var item in usuarios)
        {
            ListaUsuariosVM.Add(new UsuarioViewModel(item));
        }
    }
    public ListaUsuariosViewModel(List<usuario> usuarios, bool control)
    {
        this.ListaUsuariosVM= new List<UsuarioViewModel>();
        foreach (var item in usuarios)
        {
            ListaUsuariosVM.Add(new UsuarioViewModel(item));
        }
    }
    public ListaUsuariosViewModel()
    {
    }
}