using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="Bạn phải nhập Username")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Bạn phải nhập Email"),EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password),Required(ErrorMessage ="Bạn phải nhập Password")]
		public string Password { get; set; }
	}
}
