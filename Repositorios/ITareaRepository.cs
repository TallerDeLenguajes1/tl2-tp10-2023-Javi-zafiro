using tl2_tp10_2023_Javi_zafiro.Models;
using tl2_tp10_2023_Javi_zafiro.ViewModels;
namespace repositorioParaKamba;

public interface ITareaRepository
{
    public void CrearTarea(tarea tar);
    public void ModificarTarea(int idTarea, tarea tar);
    public tarea ObtenerTarea(int idTarea);
    public List<TareaViewModel> ListarTareasPorUsuario(int idUsuario);
    public List<TareaViewModel> ListarTareasPorTablero(int idTablero);
    public void BorrarTarea(int idTarea);
    public void AsignarTareaAUsuario(int? idUsuario, int idTarea);
    public void CambiarEstado(int idTarea, EstadoTarea estado);
    public List<tarea> ListarTodasTareas();
}