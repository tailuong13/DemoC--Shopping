using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBanHang1.Data;
using ShopBanHang1.ViewModels;

namespace ShopBanHang1.Controllers
{
	public class HangHoaController : Controller
	{
		private readonly ShopContext db;
		public HangHoaController(ShopContext context)
		{
			db = context;
		}
		public IActionResult Index(int? loai)
		{
			var hangHoa = db.HangHoas.AsQueryable();

			if (loai.HasValue)
			{
				hangHoa = hangHoa.Where(p => p.MaLoai == loai);
			}

			var data = hangHoa.Select(p => new HangHoaViewModel
			{
				MaHh = p.MaHh,
				TenHh = p.TenHh,
				DonGia = p.DonGia ?? 0,
				Hinh = p.Hinh ?? "",
				MoTa = p.MoTaDonVi ?? "",
				TenLoai = p.MaLoaiNavigation.TenLoai
			});

			return View(data);
		}

		public IActionResult Search(string? query)
		{
			var hangHoa = db.HangHoas.AsQueryable();

			if (query != null)
			{
				hangHoa = hangHoa.Where(p => p.TenHh.Contains(query));
			}

			var data = hangHoa.Select(p => new HangHoaViewModel
			{
				MaHh = p.MaHh,
				TenHh = p.TenHh,
				DonGia = p.DonGia ?? 0,
				Hinh = p.Hinh ?? "",
				MoTa = p.MoTaDonVi ?? "",
				TenLoai = p.MaLoaiNavigation.TenLoai
			});

			return View(data);
		}

		public IActionResult Detail(int id)
		{
			var hangHoa = db.HangHoas
				.Include(p => p.MaLoaiNavigation)
				.SingleOrDefault(p => p.MaHh == id);

			if (hangHoa == null)
			{
				TempData["Message"] = "Không tìm thấy sản phẩm có mã " + id.ToString();
				return Redirect("/404");
			}

			var data = new ChiTietHangHoaViewModel
			{
				MaHh = hangHoa.MaHh,
				TenHh = hangHoa.TenHh,
				DonGia = hangHoa.DonGia ?? 0,
				Hinh = hangHoa.Hinh ?? "",
				MoTaNgan = hangHoa.MoTaDonVi ?? "",
				TenLoai = hangHoa.MaLoaiNavigation.TenLoai,
				ChiTiet = hangHoa.MoTa ?? "",
				DanhGia = 5,
				SoLuongConLai = 10
			};
			return View(data);
		}
	}
}
