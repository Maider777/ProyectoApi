using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class MonedaItem
    {
        [Key]
        public string Nombre { get; set; }
        public float Ultimo { get; set; }
        public float Max { get; set; }
    }
}