using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class ReservationStatus
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
