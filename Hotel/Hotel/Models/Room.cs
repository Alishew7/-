using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
	public class Room
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "The Room number field is required")]
		[Display(Name = "Номер на стая")]
		public int RoomNumber { get; set; }

		[ForeignKey("Floor")]
		public int FloorId;
		public Floor? Floor { get; set; }

		[ForeignKey("RoomType")]
		[Display(Name = "Изберете вид на стаята")]
		public int? RoomTypeId;
		public RoomType? RoomType { get; set; }
    }
}
