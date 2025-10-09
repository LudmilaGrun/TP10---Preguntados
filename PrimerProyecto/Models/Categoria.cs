public class Categoria{

    public int IdCategoria { get; set; }
    public string Nombre { get; set; } 
    public string Foto { get; set; }
    
    public Categoria(string pNombre,string pFoto)
    {
        Nombre = pNombre;
        Foto = pFoto;   
    }
}