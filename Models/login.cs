using tl2_tp10_2023_Javi_zafiro.ViewModels;

namespace tl2_tp10_2023_Javi_zafiro.Models;

public class login
{
    private string nombre;
    private string contrasenia;
    public string Nombre { get => nombre; set => nombre = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public login(LoginViewModel usu)
    {
        this.Nombre=usu.Nombre;
        this.Contrasenia=usu.Contrasenia;
    }
    public login()
    {
    }
}