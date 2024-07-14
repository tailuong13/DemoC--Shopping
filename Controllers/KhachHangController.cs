using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyCommerce.Models;
using ShopBanHang1.Data;
using ShopBanHang1.Helpers;
using ShopBanHang1.ViewModels;

namespace ShopBanHang1.Controllers
{
	public class KhachHangController : Controller
	{
		public readonly ShopContext db;
		public readonly IMapper _mapper;
		public KhachHangController(ShopContext context, IMapper mapper)
		{
			db = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult DangKy()
		{
			return View();
		}

		[HttpPost]
		public IActionResult DangKy(DangKyViewModel model, IFormFile Hinh)
		{
			if (ModelState.IsValid) {
				var khachHang = _mapper.Map<KhachHang>(model);
				khachHang.RandomKey = Util.GenerateRandomKey();
				khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
				khachHang.HieuLuc = true;
				khachHang.VaiTro = 0;

				if (Hinh != null) 
				{
					khachHang.Hinh = Util.UploadHinh(Hinh, "KhachHang");
				}

				db.Add(khachHang);
				db.SaveChanges();
				return RedirectToAction("Index", "Home");
			}
			return View();
		}
	}
}
