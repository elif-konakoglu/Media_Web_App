using Alpata.API.Business.Interfaces;

namespace Alpata.API.Business
{
	public class FileHelper : IFileHelper
	{
        public string UploadFile(IFormFile file)
        {
            var filePath = "API/Files/";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (file != null && file.Length > 0)
            {
                filePath = filePath + file.FileName;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return filePath;
        }
    }
}