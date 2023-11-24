using modelosParaKamba;
namespace repositorioParaKamba;

public interface ITareaRepository
{
    public tarea CrearTarea(int idTablero, tarea tar);
    public void ModificarTarea(int idTarea, tarea tar);
    public tarea ObtenerTarea(int idTarea);
    public List<tarea> ListarTareasPorUsuario(int idUsuario);
    public List<tarea> ListarTareasPorTablero(int idTablero);
    public void BorrarTarea(int idTarea);
    public void AsignarTareaAUsuario(int idUsuario, int idTarea);
}