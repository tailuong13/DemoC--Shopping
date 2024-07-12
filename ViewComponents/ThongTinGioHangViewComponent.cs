using Microsoft.AspNetCore.Mvc;
using ShopBanHang1.Helpers;
using ShopBanHang1.ViewModels;

namespace ShopBanHang1.ViewComponents
{
	public class ThongTinGioHangViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var gioHang = HttpContext.Session.Get<List<GioHangViewModel>>(ConstSetting.CART_KEY) ?? new List<GioHangViewModel>();


			return View("ThongTinGioHang", new ThongTinGioHangViewModel
			{
				SoLuongDon = gioHang.Select(p => p.MaHh).Distinct().Count(),
				TongTien = gioHang.Sum(p => p.ThanhTien)
			});
		}
	}
}
