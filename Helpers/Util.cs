using System.Text;

namespace ShopBanHang1.Helpers
{
	public class Util
	{
		public static string UploadHinh(IFormFile Hinh, string folder)
		{
			try
			{
				var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, Hinh.FileName);
				using (var fileStream = new FileStream(fullPath, FileMode.CreateNew))
				{
					Hinh.CopyTo(fileStream);
				}
				return Hinh.FileName;
			}
			catch (Exception ex)
			{
				return string.Empty;
			}
		}

		public static string GenerateRandomKey(int length = 5)
		{
			string pattern = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var sb = new StringBuilder();
			var random = new Random();
			for (int i = 0; i < length; i++)
			{
				sb.Append(pattern[random.Next(0, pattern.Length)]);
			}

			return sb.ToString();
		}
	}
}
