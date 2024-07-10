namespace ShopBanHang1.ViewModels
{
    public class HangHoaViewModel
    {
        public int MaHh { get; set; }
        public string TenHh { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public string MoTa { get; set; }
        public string TenLoai { get; set; }
    }

    public class ChiTietHangHoaViewModel
    {
        public int MaHh { get; set; }
        public string TenHh { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public string MoTaNgan { get; set; }
        public string TenLoai { get; set; }
        public string ChiTiet { get; set; }
        public int DanhGia { get; set; }
        public int SoLuongConLai { get; set; }
    }
}
