using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{

	public enum UserGender
	{
        Мъж,
		Жена
	}
    public class Client
    {
        [Key]
        public int Id {  get; set; }
		[Required(ErrorMessage = "The Name field is required")]
		[MaxLength(256, ErrorMessage = "this fild cannot be more than 255 symbols")]
		[Display(Name = "Име")]
        public string Name { get; set; }
		[Required(ErrorMessage = "The Surname field is required")]
		[MaxLength(256, ErrorMessage = "this fild cannot be more than 255 symbols")]
		[Display(Name = "Презиме")]
        public string SurName { get; set; }
		[Required(ErrorMessage = "The Last Name field is required")]
		[MaxLength(256, ErrorMessage = "this fild cannot be more than 255 symbols")]
		[Display(Name = "Фамилия")]
        public string Last_Name { get; set; }
		[Required(ErrorMessage = "The Personal id field is required")]
		[MaxLength(10, ErrorMessage = "this fild cannot be more than 10 symbols")]
		[Display(Name = "ЕГН")]
        public string Personal_id { get; set; }
        [Required(ErrorMessage = "The Phone number field is required")]
		[MaxLength(13, ErrorMessage = "this fild cannot be more than 13 symbols")]
		[Display(Name = "Телефонен номер")]
        public string Phone_number { get; set; }
        [Required(ErrorMessage = "The Email field is required")]
		[MaxLength(256, ErrorMessage = "this fild cannot be more than 255 symbols")]
		[Display(Name = "Имейл")]
        public string Email_of_client { get; set; }

        [Required(ErrorMessage = "The Gender field is required")]
        [Display(Name = "Пол")]
        public UserGender Gender { get; set; }
    }
}
