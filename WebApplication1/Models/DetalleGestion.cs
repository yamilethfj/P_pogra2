namespace parcialE.Models
{
    public class DetalleGestion
    {
        public int Id { get; set; }
        public int Operador { get; set; }
        public int IdActividad { get; set; }
        public string Fechainicio { get; set; }
        public string Fechafin { get; set; }
        public int Estado { get; set; } 
    }
}
