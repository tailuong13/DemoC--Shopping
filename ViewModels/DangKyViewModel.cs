using System.ComponentModel.DataAnnotations;

namespace ShopBanHang1.ViewModels
{
	public class DangKyViewModel
	{
		[Display(Name = "Tên đăng nhập")]
		[Required(ErrorMessage = "Mã khách hàng không được để trống")]
		[MaxLength(20, ErrorMessage ="Mã khách hàng tối đa 20 ký tự")]
		public string MaKh { get; set; }

		[Display(Name = "Mật khẩu")]
		[Required(ErrorMessage = "Mật khẩu không được để trống")]
		[DataType(DataType.Password)]
		public string MatKhau { get; set; }

		[Display(Name = "Họ Tên")]
		public string HoTen { get; set; }

		[Display(Name = "Giới tính")]
		public bool GioiTinh { get; set; } = true;

		[Display(Name = "Ngày sinh")]
		[DataType(DataType.Date)]
		public DateTime? NgaySinh { get; set; }

		[Display(Name = "Địa chỉ")]
		[MaxLength(60, ErrorMessage = "Địa chỉ tối đa 60 ký tự")]
		public string DiaChi { get; set; }

		[Display(Name = "Điện thoại")]
		[MaxLength(20, ErrorMessage = "Điện thoại tối đa 20 ký tự")]
		[RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Điện thoại không đúng định dạng")]
		public string DienThoai { get; set; }

		[Display(Name = "Email")]
		[EmailAddress(ErrorMessage = "Email không đúng định dạng")]
		public string Email { get; set; }

		public string? Hinh { get; set; }
	}
}
