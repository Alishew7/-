using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
	public class Floor
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "The Level field is required")]
		[Display(Name = "Етаж")]
		public int Level { get; set; }


		[Required(ErrorMessage = "The Number of rooms field is required")]
		[Display(Name = "Брой стаи на етаж")]
		public int Number_of_rooms {  get; set; }	

		public IEnumerable<Room> Rooms { get; set; }

		public Floor()
		{

			Rooms = new List<Room>();
		}
	}
}
