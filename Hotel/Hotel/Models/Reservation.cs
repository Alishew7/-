using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
	public class Reservation
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Client")]
		[Display(Name = "ЕГН на клиента")]
		public int Client_id { get; set; }
		[Display(Name = "Client ID")]
		public Client Client { get; set; }

		[ForeignKey("Room")]
		[Display(Name = "Номер на стая")]
		public int Room_id { get; set; }
		[Display(Name = "Room ID")]
		public Room Room { get; set; }

		[Required(ErrorMessage = "The Reservation start field is required")]
		[MaxLength(256, ErrorMessage = "this fild cannot be more than 255 symbols")]
		[Display(Name = "Начална дата на резервацията")]
		public DateTime RservationStart { get; set; }
		[Required(ErrorMessage = "The Reservation end field is required")]
		[MaxLength(256, ErrorMessage = "this fild cannot be more than 255 symbols")]
		[Display(Name = "Крайна дата на резервацията")]
		public DateTime RservationEnd { get; set;}

		[Required(ErrorMessage = "The Status field is required")]
		[Display(Name = "Състояние")]
        [ForeignKey("ReservationStatus")]
        public int StatusId { get; set; }

        [Display(Name = "Състояние на резервацията")]
        public ReservationStatus ReservationStatus { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal TotalPrice { get; set; }
	}
}
