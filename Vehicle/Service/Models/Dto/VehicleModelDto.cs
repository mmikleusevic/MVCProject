using System.ComponentModel.DataAnnotations;

namespace Service.Models.Dto
{
    public class VehicleModelDto
    {
        public int Id { get; set; }
        [Display(Name = "Car Name")]
        public string CarName { get; set; }
        public string Abbreviation { get; set; }
        public int MakeId { get; set; }
    }
}
