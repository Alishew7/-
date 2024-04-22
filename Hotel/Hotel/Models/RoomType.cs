using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
	public class RoomType
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "The Title field is required")]
		[MaxLength(255, ErrorMessage = "this fild cannot be more than 255 symbols")]
		[Display(Name = "Заглавие")]
		public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

		[Required(ErrorMessage = "The Number of beds field is required")]
		[Display(Name = "Брой легла")]
		public int Number_of_beds { get; set; }

		[Display(Name = "Има ли тераса")]
		public bool Does_it_have_balcony { get; set; } = false;

		[Required(ErrorMessage = "The Price field is required")]
		[Range(0,99999, ErrorMessage ="Invalid Price")]
		[Display(Name = "Цена")]
		public decimal Price { get; set; }


		public ICollection<Room> ?Rooms { get; set; }
	}
}
