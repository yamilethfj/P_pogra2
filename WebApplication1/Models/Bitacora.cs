namespace WebApplication1.Models
{
    public class Bitacora
    {
        public int Id { get; set; }
        public string Tabla { get; set; }
        public int IdTabla { get; set; }
        public string CamposJson { get; set; }
        public int Usuario { get; set; }
        public string Fecha { get; set; }
    }
}
