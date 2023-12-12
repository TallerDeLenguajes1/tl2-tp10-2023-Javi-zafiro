using tl2_tp10_2023_Javi_zafiro.Models;
using System.Data.SQLite;

namespace repositorioParaKamba;

public class TableroRepository : ITableroRepository
{
    private readonly string connectionString;

    public TableroRepository(string CadenaDeConexion)
    {
        connectionString = CadenaDeConexion;
    }
    public void CrearTablero(tablero tab){
        try
        {
            var query = "INSERT INTO tablero (id_usuario_asignado, nombre, descripcion) VALUES (@idusu, @nombre, @descripcion);";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command= new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idusu", tab.IdUsuariPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tab.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tab.Descripcion));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        catch (System.Exception)
        {
            
            throw new Exception($"No se pudo crear el tablero") ;
        }
        
    }
    public void ModificarTablero(int idTablero, tablero tab){
        try
        {
            var query = "UPDATE tablero SET id_usuario_asignado=@idusu, nombre = @nombre, descripcion = @descripcion WHERE id = @idtablero;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command= new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idusu", tab.IdUsuariPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tab.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tab.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@idtablero", idTablero));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        catch (System.Exception)
        {
            
            throw new Exception($"No se pudo modificar el tablero");
        }
        
    }
    public tablero ObtenerTablero(int idTablero){
        tablero tab = null;
        var query="SELECT * FROM tablero WHERE id= @idtablero;";
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idtablero", idTablero));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    tab.Id=Convert.ToInt32(reader["id"]);
                    tab.IdUsuariPropietario=Convert.ToInt32(reader["id_usuario_asignado"]);
                    tab.Nombre=reader["nombre"].ToString();
                    tab.Descripcion=reader["descripcion"].ToString();
                }
            }
            connection.Close();
        }
        if (tab==null)
            throw new Exception("Tablero no encontrado.");
        return(tab);
    }
    public List<tablero> ListarTableros(){
         var query="SELECT * FROM tablero;";
        List<tablero> listaDeTableros = new List<tablero>();
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var tab = new tablero();
                    tab.Id = Convert.ToInt32(reader["id"]);
                    tab.IdUsuariPropietario=Convert.ToInt32(reader["id_usuario_asignado"]);
                    tab.Nombre=reader["nombre"].ToString();
                    tab.Descripcion=reader["descripcion"].ToString();
                    listaDeTableros.Add(tab);
                }
            }
            connection.Close();
        }
        if (listaDeTableros.Count<=0)
            throw new Exception("Lista Vacia.");
        return (listaDeTableros);
    }
    public List<tablero> ListarTablerosDeUsuario(int idUsuario){
        var query="SELECT * FROM tablero WHERE id_usuario_asignado = @idusu;";
        List<tablero> listaDeTableros = new List<tablero>();
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idusu", idUsuario));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var tab = new tablero();
                    tab.Id = Convert.ToInt32(reader["id"]);
                    tab.IdUsuariPropietario=Convert.ToInt32(reader["id_usuario_asignado"]);
                    tab.Nombre=reader["nombre"].ToString();
                    tab.Descripcion=reader["descripcion"].ToString();
                    listaDeTableros.Add(tab);
                }
            }
            connection.Close();
        }
        if (listaDeTableros.Count<=0)
            throw new Exception("Lista Vacia.");
        return (listaDeTableros);
    }
    public void BorrarTablero(int idTablero){
        try
        {
            var query="DELETE FROM tablero WHERE id=@idtablero;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command= new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idtablero", idTablero));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        catch (System.Exception)
        {
            
            throw new Exception($"No se pudo borrar el tablero");
        }
        
    }
}