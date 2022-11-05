namespace WebApplication1.Models
{
    public class DetalleActividad
    {
        public int Id { get; set; }
        public int IdDetalleGestion { get; set; }
        public int IdAccion { get; set; }
        public double Porcentaje { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int Estado { get; set; }
    }
}
