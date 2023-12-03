namespace tl2_tp10_2023_Javi_zafiro.Models;

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
}