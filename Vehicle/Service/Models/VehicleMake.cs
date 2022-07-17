using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class VehicleMake
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
