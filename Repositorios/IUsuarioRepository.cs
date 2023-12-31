using tl2_tp10_2023_Javi_zafiro.Models;
namespace repositorioParaKamba;

public interface IUsuarioRepository
{
    public void CrearUsuario(usuario usu);
    public void ModificarUsuario(int idUsuario, usuario usu);
    public List<usuario> ListarUsuarios();
    public usuario ObtenerUsuario(int idUsuario);
    public void BorrarUsuario(int idUsuario);
    public usuario ObtenerUsuarioLogin(string nombre, string contrasenia);
}