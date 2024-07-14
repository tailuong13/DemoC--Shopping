using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ShopBanHang1.Data;
using ShopBanHang1.ViewModels;

namespace ShopBanHang1.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() 
		{
			CreateMap<DangKyViewModel, KhachHang>();
				//.ForMember(kh => kh.HoTen, option => option.MapFrom(DangKyViewModel => DangKyViewModel.HoTen))
				//.ReverseMap();
		}
	}
}
