namespace tl2_tp10_2023_Javi_zafiro.Models;
using tl2_tp10_2023_Javi_zafiro.ViewModels;

public enum TiposUsuario
{
    Administrador = 1,
    Operador = 2
}

public class usuario
{
    private int id;
    private string nombreDeUsuario;
    private TiposUsuario tipo;
    private string contrasenia;

    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public int Id { get => id; set => id = value; }
    public TiposUsuario Tipo { get => tipo; set => tipo = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }

    public usuario(UsuarioViewModel usuario){
        this.id = usuario.Id;
        this.nombreDeUsuario = usuario.NombreDeUsuario;
        this.contrasenia = usuario.Contrasenia;
        this.tipo = usuario.Tipo;
    }

    public usuario(){}
}