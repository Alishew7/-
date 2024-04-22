using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models.View_Models
{
    public class ReservationVM
    {
        public int Id { get; set; }
		[Display(Name = "ЕГН на клиента")]
		public int Client_id { get; set; }
		[Display(Name = "Номер на стая")]
		public int Room_id { get; set; }

        [Required(ErrorMessage = "The Reservation start field is required")]
        [Display(Name = "Начална дата на резервацията")]
        public DateTime RservationStart { get; set; }

        [Required(ErrorMessage = "The Reservation end field is required")]
        [Display(Name = "Крайна дата на резервацията")]
        public DateTime RservationEnd { get; set; }

        [Required(ErrorMessage = "The Status field is required")]
        [Display(Name = "Статус")]
        public int statusId { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal TotalPrice { get; set; }

        public List<Client>? Clients { get; set; }
        public List<Room>? Rooms { get; set;}
        public List<ReservationStatus>? Statuses { get; set; }
    }
}
