using modelosParaKamba;
using System.Data.SQLite;

namespace repositorioParaKamba;

public class TareaRepository : ITareaRepository
{
    private string cadenaConexion="Data Source=DB/kamba.db;Cache=Shared";
    public tarea CrearTarea(int idTablero, tarea tar){
        tar.IdTablero=idTablero;
        var query = "INSERT INTO tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@idtab, @nombre, @estado, @descripcion, @color, @idusu);";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idtab", tar.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", tar.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", tar.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tar.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", tar.Color));
            command.Parameters.Add(new SQLiteParameter("@idusu", tar.Usuario_asignado));
            command.ExecuteNonQuery();
            connection.Close();
            return tar;
        }
    }
    public void ModificarTarea(int idTarea, tarea tar){
        var query = "UPDATE tarea SET id_tablero = @idtab, nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @idusu WHERE id = @idtar;";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idtar", idTarea));
            command.Parameters.Add(new SQLiteParameter("@idtab", tar.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", tar.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", tar.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tar.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", tar.Color));
            command.Parameters.Add(new SQLiteParameter("@idusu", tar.Usuario_asignado));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public tarea ObtenerTarea(int idTarea){
        var tar = new tarea();
        var query="SELECT * FROM tarea WHERE id= @idtar;";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idtar", idTarea));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    tar.Id=Convert.ToInt32(reader["id"]);
                    tar.IdTablero=Convert.ToInt32(reader["id_tablero"]);
                    tar.Nombre=reader["nombre"].ToString();
                    tar.Estado=(EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado"].ToString());
                    tar.Descripcion=reader["descripcion"].ToString();
                    tar.Color=reader["color"].ToString();
                    tar.Usuario_asignado=Convert.ToInt32(reader["id_usuario_asignado"]);
                }
            }
            connection.Close();
        }
        return(tar);
    }
    public List<tarea> ListarTareasPorUsuario(int idUsuario){
        var query="SELECT * FROM tarea WHERE id_usuario_asignado = @idusu;";
        List<tarea> listaDeTareasXUsuario = new List<tarea>();
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idusu", idUsuario));
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
                    listaDeTareasXUsuario.Add(tar);
                }
            }
            connection.Close();
        }
        return (listaDeTareasXUsuario);
    }
    public List<tarea> ListarTareasPorTablero(int idTablero){
        var query="SELECT * FROM tarea WHERE id_tablero = @idtab;";
        List<tarea> listaDeTareasXTablero = new List<tarea>();
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idtab", idTablero));
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
                    listaDeTareasXTablero.Add(tar);
                }
            }
            connection.Close();
        }
        return (listaDeTareasXTablero);
    }
    public void BorrarTarea(int idTarea){
        var query="DELETE FROM tarea WHERE id=@idtar;";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idtar", idTarea));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void AsignarTareaAUsuario(int idUsuario, int idTarea){
        var query="UPDATE tarea SET id_usuario_asignado=@idusu WHERE id=@idtar";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idusu", idUsuario));
            command.Parameters.Add(new SQLiteParameter("@idtar", idTarea));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}