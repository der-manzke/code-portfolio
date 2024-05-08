using Infrastructure;
using Microsoft.Win32;

namespace CodePortfolio.Helper
{
	public class FileHelper
	{
		public static void DecodeBase64ToFile(string base64Data, string outputPath, string fileName)
		{
			if (!Directory.Exists(outputPath))
			{
				Directory.CreateDirectory(outputPath);
			}

			if (System.IO.File.Exists(outputPath + fileName))
			{
				System.IO.File.Delete(outputPath + fileName);
			}

			if (!string.IsNullOrEmpty(base64Data))
			{
				byte[] bytes = Convert.FromBase64String(base64Data.Split(",")[1]);

				System.IO.File.WriteAllBytes(outputPath + fileName, bytes);
			}
		}

		public static string EncodeFileToBase64(string filePath)
		{
			if (filePath != string.Empty)
			{
				try
				{
					byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
					string base64String = Convert.ToBase64String(fileBytes);
					return "data:" + getContentType(filePath) + ";base64," + base64String;
				}
				catch 
				{
					return string.Empty;
				}
			}
			return string.Empty;
		}

		private static Random random = new Random();

		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public static string getContentType(string filePath)
		{
			try
			{
				string extension = Path.GetExtension(filePath);
				string registryKey = $@"HKEY_CLASSES_ROOT\{extension}";
				return Registry.GetValue(registryKey, "Content Type", null) as string;

			}
			catch (Exception e)
			{
				return e.ToString();
			}
		}

		public static async Task<string> WriteFileToPath(string filePath, IFormFile image) {

			try 
			{
				using (Stream fileStream = new FileStream(filePath, FileMode.Create))
				{
					await image.CopyToAsync(fileStream);
				}
				return "OK";
			} 
			catch (Exception e)
			{
				Log.AddLog4NetEntry("ERROR", "File could not be added: " + e.ToString());
				return "ERROR";
			}
		}
	}
}
