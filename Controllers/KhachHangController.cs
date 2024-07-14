using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

		#region Register
		[HttpGet]
		public IActionResult DangKy()
		{
			return View();
		}

		[HttpPost]
		public IActionResult DangKy(DangKyViewModel model, IFormFile Hinh)
		{
			if (ModelState.IsValid)
			{
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
		#endregion

		#region Login
		[HttpGet]
		public IActionResult DangNhap(string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> DangNhap(DangNhapViewModel model, string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			if (ModelState.IsValid)
			{
				var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.Username);
				if (khachHang == null)
				{
					ModelState.AddModelError("Lỗi", "Tài khoản không tồn tại");
				}
				else
				{
					if (!khachHang.HieuLuc)
					{
						ModelState.AddModelError("Lỗi", "Tài khoản đã bị khóa");
					}
					else
					{
						if (khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey)) {
							ModelState.AddModelError("Lỗi", "Mật khẩu không đúng");
						}
						else
						{
							var claims = new List<Claim>
							{
								new Claim(ClaimTypes.Email, khachHang.Email),
								new Claim(ClaimTypes.Name, khachHang.HoTen),
								new Claim("CustomerID", khachHang.MaKh),
								new Claim(ClaimTypes.Role, "Customer"),
							};

							var claimsIdentity = new ClaimsIdentity(
								claims, CookieAuthenticationDefaults.AuthenticationScheme);
							var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

							await HttpContext.SignInAsync(claimsPrincipal);
							if (Url.IsLocalUrl(ReturnUrl))
							{
								return Redirect(ReturnUrl);
							}
							else
							{
								return Redirect("/");
							}
						}
					}
				}
			}
			return View();
		}
		#endregion
		
		[Authorize]
		public IActionResult ThongTinTaiKhoan()
		{
			return View();
		}

		[Authorize]
		public async Task<IActionResult> DangXuat()
		{
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}
	}
}
