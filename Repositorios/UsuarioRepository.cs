using tl2_tp10_2023_Javi_zafiro.Models;
using System.Data.SQLite;

namespace repositorioParaKamba;

public class UsuarioRepository : IUsuarioRepository
{
    private string cadenaConexion="Data Source=DB/kamba.db;Cache=Shared";

    public void CrearUsuario(usuario usu){
        var query = "INSERT INTO usuario (nombre, tipo, contrasenia) VALUES (@nombre, @tipo, @contrasenia);";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@nombre", usu.NombreDeUsuario));
            command.Parameters.Add(new SQLiteParameter("@tipo", (int)usu.Tipo));
            command.Parameters.Add(new SQLiteParameter("@contrasenia", usu.Contrasenia));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void ModificarUsuario(int idUsuario, usuario usu){
        var query = "UPDATE usuario SET nombre = @nombre, tipo = @tipo WHERE id = @idusu;";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@nombre", usu.NombreDeUsuario));
            command.Parameters.Add(new SQLiteParameter("@tipo", (int)usu.Tipo));
            command.Parameters.Add(new SQLiteParameter("@idusu", idUsuario));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public List<usuario> ListarUsuarios(){
        var query="SELECT * FROM usuario;";
        List<usuario> listaDeUsuarios = new List<usuario>();
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var usu = new usuario();
                    usu.Id= Convert.ToInt32(reader["id"]);
                    usu.NombreDeUsuario=reader["nombre"].ToString();
                    usu.Tipo=(TiposUsuario)Enum.Parse(typeof(TiposUsuario), reader["tipo"].ToString());
                    listaDeUsuarios.Add(usu);
                }
            }
            connection.Close();
        }
        return (listaDeUsuarios);
    }
    public usuario ObtenerUsuario(int idUsuario){
        var usu = new usuario();
        var query="SELECT * FROM usuario WHERE id= @idusu;";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idusu", idUsuario));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    usu.Id=Convert.ToInt32(reader["id"]);
                    usu.NombreDeUsuario=reader["nombre"].ToString();
                    usu.Tipo=(TiposUsuario)Enum.Parse(typeof(TiposUsuario), reader["tipo"].ToString());
                }
            }
            connection.Close();
        }
        return(usu);
    }

    public usuario ObtenerUsuarioLogin(string nombre, string contrasenia){
        var usu = new usuario();
        var query="SELECT * FROM usuario WHERE nombre= @nombre AND contrasenia=@contra;";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@nombre", nombre));
            command.Parameters.Add(new SQLiteParameter("@contra", contrasenia));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    usu.Id=Convert.ToInt32(reader["id"]);
                    usu.NombreDeUsuario=reader["nombre"].ToString();
                    usu.Tipo=(TiposUsuario)Enum.Parse(typeof(TiposUsuario), reader["tipo"].ToString());
                }
            }
            connection.Close();
        }
        return(usu);
    }
    public void BorrarUsuario(int idUsuario){
        var query="DELETE FROM usuario WHERE id=@idusu;";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command= new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idusu", idUsuario));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}