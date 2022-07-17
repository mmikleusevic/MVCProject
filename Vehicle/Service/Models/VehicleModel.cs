using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        [ForeignKey("Make")]
        public int MakeId { get; set; }
        public virtual VehicleMake Make { get; set; }
    }
}
