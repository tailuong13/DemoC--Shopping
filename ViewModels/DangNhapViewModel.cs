using System.ComponentModel.DataAnnotations;

namespace ShopBanHang1.ViewModels
{
	public class DangNhapViewModel
	{
		[Display(Name = "Tên đăng nhập")]
		[Required(ErrorMessage = "Tên đăng nhập không được để trống")]
		[MaxLength(20, ErrorMessage = "Tên đăng nhập tối đa 20 ký tự")]
		public string Username { get; set; }
		[Display(Name = "Mật khẩu")]
		[Required(ErrorMessage = "Mật khẩu không được để trống")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
