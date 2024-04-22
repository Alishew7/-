using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models.View_Models
{
    public class RoomVM
    {
		[Required(ErrorMessage = "The Room number field is required")]
		[Display(Name = "Номер на стая")]
        public int RoomNumber { get; set; }

		[Required(ErrorMessage = "You must select a floor")]
		[Display(Name = "Изберете етаж")]
        public int FloorId { get; set; }

		[Required(ErrorMessage = "You must select a room type")]
		[Display(Name = "Изберете вид на стаята")]
        public int? RoomTypeId { get; set; }


        public List<Floor>? floors = new List<Floor>();
        public List<RoomType>? roomTypes = new List<RoomType>();
    }

}
