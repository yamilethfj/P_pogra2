namespace parcialE.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string comentario { get; set; }
        public int IdDetalleGestion { get; set; }
        public int IdPersona { get; set; }
        public int Estado { get; set; }
        
    }
}
