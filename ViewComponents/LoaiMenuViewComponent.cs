using Microsoft.AspNetCore.Mvc;
using ShopBanHang1.Data;
using ShopBanHang1.ViewModels;

namespace ShopBanHang1.ViewComponents 
{
    public class LoaiMenuViewComponent : ViewComponent
    {
        private readonly ShopContext db;

        public LoaiMenuViewComponent(ShopContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(loai => new LoaiMenuViewModel
            {
                MaLoai = loai.MaLoai,
                TenLoai = loai.TenLoai,
                SoLuong = loai.HangHoas.Count
            });

            return View(data);
        }
    }
}
