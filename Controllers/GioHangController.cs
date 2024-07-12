using Microsoft.AspNetCore.Mvc;
using ShopBanHang1.Data;
using ShopBanHang1.Helpers;

using ShopBanHang1.ViewModels;

namespace ShopBanHang1.Controllers
{
	public class GioHangController : Controller
	{
		private readonly ShopContext db;
		public GioHangController(ShopContext context)
		{
			db = context;
		}

		public List<GioHangViewModel> Cart => HttpContext.Session.Get<List<GioHangViewModel>>(ConstSetting.CART_KEY) ?? new List<GioHangViewModel>();

		public IActionResult Index()
		{
			return View(Cart);
		}

		public IActionResult AddToCart(int id, int quantity = 1)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.MaHh == id);

			if (item == null)
			{
				var hangHoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
				if (hangHoa == null)
				{
					TempData["Message"] = "Không tìm thấy hàng hóa có mã " + id;
					Redirect("/404");
				}

				item = new GioHangViewModel
				{
					MaHh = hangHoa.MaHh,
					TenHh = hangHoa.TenHh,
					DonGia = hangHoa.DonGia ?? 0,
					Hinh = hangHoa.Hinh ?? "",
					SoLuong = quantity
				};
				gioHang.Add(item);

			}
			else
			{
				item.SoLuong += quantity;
			}
			HttpContext.Session.Set(ConstSetting.CART_KEY, gioHang);
			return RedirectToAction("Index");
		}

		public IActionResult RemoveCart(int id)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.MaHh == id);

			if (item != null)
			{
				gioHang.Remove(item);
				HttpContext.Session.Set(ConstSetting.CART_KEY, gioHang);
			}
			return RedirectToAction("Index");
		}
	}
}
