using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{

	public enum Role
	{
        Admin,
		Receptionist
    }
	[Table(name:"Users")]
	public class User
	{
		[Key] 
		public int Id { get; set; }

		[Required(ErrorMessage = "The username field is required")]
		[MaxLength(256, ErrorMessage = "this fild cannot be more than 255 symbols")]
		[Display(Name = "Потребителско име")]
		public string UserName { get; set; }


		[Required(ErrorMessage = "The password field is required")]
		[MaxLength(256, ErrorMessage = "this fild cannot be more than 255 symbols")]
		[DataType(DataType.Password)]
		[Display(Name = "Парола")]
		public string Password { get; set; }
		public Role Role { get;set; } 
	}
}
