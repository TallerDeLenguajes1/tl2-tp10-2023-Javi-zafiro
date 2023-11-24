namespace modelosParaKamba;

public class tablero
{
    private int id;
    private int idUsuariPropietario;
    private string nombre;
    private string descripcion;

    public int Id { get => id; set => id = value; }
    public int IdUsuariPropietario { get => idUsuariPropietario; set => idUsuariPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
}