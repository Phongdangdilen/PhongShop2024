using System.ComponentModel.DataAnnotations;

namespace Shopping.Models.ViewModel
{
	public class LoginViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Bạn phải nhập Username")]
		public string Username { get; set; }
		[DataType(DataType.Password), Required(ErrorMessage = "Bạn phải nhập Password")]
		public string Password { get; set; }
		public string ReturnUrl { get; set; }
	}
}
