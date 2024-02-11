using tl2_tp10_2023_Javi_zafiro.Models;
namespace repositorioParaKamba;

public interface ITableroRepository
{
    public void CrearTablero(tablero tab);
    public void ModificarTablero(int idTablero, tablero tab);
    public tablero ObtenerTablero(int idTablero);
    public List<tablero> ListarTableros();
    public List<tablero> ListarTablerosPorTareas(List<tarea> list);
    public List<tablero> ListarTablerosDeUsuario(int idUsuario);
    public void BorrarTablero(int idTablero);
}