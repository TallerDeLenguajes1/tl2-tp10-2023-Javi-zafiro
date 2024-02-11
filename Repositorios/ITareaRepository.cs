using tl2_tp10_2023_Javi_zafiro.Models;
namespace repositorioParaKamba;

public interface ITareaRepository
{
    public void CrearTarea(tarea tar);
    public void ModificarTarea(int idTarea, tarea tar);
    public tarea ObtenerTarea(int idTarea);
    public List<tarea> ListarTareasPorUsuario(int idUsuario);
    public List<tarea> ListarTareasPorTablero(int idTablero);
    public void BorrarTarea(int idTarea);
    public void AsignarTareaAUsuario(int? idUsuario, int idTarea);
    public List<tarea> ListarTodasTareas();
}