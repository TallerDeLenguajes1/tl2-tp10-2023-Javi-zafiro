using tl2_tp10_2023_Javi_zafiro.Models;
using tl2_tp10_2023_Javi_zafiro.ViewModels;
using System.Data.SQLite;

namespace repositorioParaKamba;

public class TareaRepository : ITareaRepository
{
    private readonly string connectionString;

    public TareaRepository(string CadenaDeConexion)
    {
        connectionString = CadenaDeConexion;
    }
    public void CrearTarea(tarea tar){
        try
        {
            var query = "INSERT INTO tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@idtab, @nombre, @estado, @descripcion, @color, @idusu);";
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idtab", tar.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", tar.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", (int)tar.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tar.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", tar.Color));
            command.Parameters.Add(new SQLiteParameter("@idusu", tar.Usuario_asignado));
            command.ExecuteNonQuery();
            connection.Close();
            
        }
        }
        catch (System.Exception)
        {
            
            throw new Exception($"No se pudo crear la tarea.");
        }
        
    }
    public void ModificarTarea(int idTarea, tarea tar){
        try
        {
            var query = "UPDATE tarea SET id_tablero = @idtab, nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @idusu WHERE id = @idtar;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command= new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idtar", idTarea));
                command.Parameters.Add(new SQLiteParameter("@idtab", tar.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tar.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", (int)tar.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tar.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tar.Color));
                command.Parameters.Add(new SQLiteParameter("@idusu", tar.Usuario_asignado));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        catch (System.Exception)
        {
            
            throw new Exception($"No se pudo modificar la tarea");
        }
    }

    public void CambiarEstado(int idTarea, EstadoTarea estado){
        try
        {
            var query="UPDATE tarea SET estado=@est WHERE id=@idtar";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command= new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@est", (int)estado));
                command.Parameters.Add(new SQLiteParameter("@idtar", idTarea));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        catch (System.Exception)
        {
            
            throw new Exception($"No se pudo cambiar el estado");
        }
    }
    public tarea ObtenerTarea(int idTarea){
        tarea tar = null;
        var query="SELECT * FROM tarea WHERE id= @idtar;";
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idtar", idTarea));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    tar= new tarea();
                    tar.Id=Convert.ToInt32(reader["id"]);
                    tar.IdTablero=Convert.ToInt32(reader["id_tablero"]);
                    tar.Nombre=reader["nombre"].ToString();
                    tar.Estado=(EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado"].ToString());
                    tar.Descripcion=reader["descripcion"].ToString();
                    tar.Color=reader["color"].ToString();
                    if (!reader.IsDBNull(reader.GetOrdinal("id_usuario_asignado"))) {
                        tar.Usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    } else {
                        tar.Usuario_asignado = null;
                    }
                }
            }
            connection.Close();
        }
        if (tar==null|| string.IsNullOrEmpty(tar.Nombre))
            throw new Exception("No se encontro la tarea");
        return(tar);
    }
    public List<TareaViewModel> ListarTareasPorUsuario(int idUsuario){
        var query="SELECT tarea.id AS id, id_tablero, tarea.nombre AS nombre, descripcion, estado, color, usuario.nombre AS usu FROM tarea INNER JOIN usuario on id_usuario_asignado=usuario.id WHERE id_usuario_asignado = @idusu;";
        List<TareaViewModel> listaDeTareasXUsuario = new List<TareaViewModel>();
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idusu", idUsuario));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var tar = new TareaViewModel();
                    tar.Id = Convert.ToInt32(reader["id"]);
                    tar.IdTablero=Convert.ToInt32(reader["id_tablero"]);
                    tar.Nombre=reader["nombre"].ToString();
                    tar.Descripcion=reader["descripcion"].ToString();
                    tar.Color =reader["color"].ToString();
                    tar.Estado=(EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado"].ToString());
                    //tar.Usuario_asignado=Convert.ToInt32(reader["id_usuario_asignado"]);
                    tar.Nombre_usuario=reader["usu"].ToString();
                    listaDeTareasXUsuario.Add(tar);
                }
            }
            connection.Close();
        }
        return (listaDeTareasXUsuario);
    }
    public List<TareaViewModel> ListarTareasPorTablero(int idTablero){
        var query="SELECT tarea.id AS id, id_tablero, tarea.nombre AS nombre, descripcion, id_usuario_asignado, estado, color, usuario.nombre AS usu FROM tarea LEFT JOIN usuario on id_usuario_asignado=usuario.id WHERE id_tablero = @idtab;";
        List<TareaViewModel> listaDeTareasXTablero = new List<TareaViewModel>();
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idtab", idTablero));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var tar = new TareaViewModel();
                    tar.Id = Convert.ToInt32(reader["id"]);
                    tar.IdTablero=Convert.ToInt32(reader["id_tablero"]);
                    tar.Nombre=reader["nombre"].ToString();
                    tar.Descripcion=reader["descripcion"].ToString();
                    tar.Color =reader["color"].ToString();
                    tar.Estado=(EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado"].ToString());
                    if (!reader.IsDBNull(reader.GetOrdinal("id_usuario_asignado"))) {
                        tar.Usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tar.Nombre_usuario=reader["usu"].ToString();
                    } else {
                        tar.Usuario_asignado = null;
                        tar.Nombre_usuario="Sin Asignar";
                    }
                    listaDeTareasXTablero.Add(tar);
                }
            }
            connection.Close();
        }
        return (listaDeTareasXTablero);
    }
    public void BorrarTarea(int idTarea){
        try
        {
            var query="DELETE FROM tarea WHERE id=@idtar;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command= new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idtar", idTarea));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        catch (System.Exception)
        {
            
            throw new Exception($"No se pudo borrar la tarea");
        }
        
    }
    public void AsignarTareaAUsuario(int? idUsuario, int idTarea){
        
        try
        {
            var query="UPDATE tarea SET id_usuario_asignado=@idusu WHERE id=@idtar";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command= new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idusu", idUsuario));
                command.Parameters.Add(new SQLiteParameter("@idtar", idTarea));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        catch (System.Exception)
        {
            
            throw new Exception($"No se pudo asignar el usuario");
        }
    }
    public List<tarea> ListarTodasTareas(){
        var query="SELECT * FROM tarea;";
        List<tarea> listaDeTareas = new List<tarea>();
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var tar = new tarea();
                    tar.Id = Convert.ToInt32(reader["id"]);
                    tar.IdTablero=Convert.ToInt32(reader["id_tablero"]);
                    tar.Nombre=reader["nombre"].ToString();
                    tar.Descripcion=reader["descripcion"].ToString();
                    tar.Color =reader["color"].ToString();
                    tar.Estado=(EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado"].ToString());
                    tar.Usuario_asignado=Convert.ToInt32(reader["id_usuario_asignado"]);
                    listaDeTareas.Add(tar);
                }
            }
            connection.Close();
        }
        if (listaDeTareas.Count<=0)
            throw new Exception("Lista Vacia");
        return (listaDeTareas);
    }
}