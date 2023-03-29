namespace WebApplication2.Models
{
    public static class Document
    {

        public static string UploadFile(IFormFile file, string folderName)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
            var FileName = $"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";
            var FilePath = Path.Combine(FolderPath, FileName);
            using var fs = new FileStream(FilePath, FileMode.Create);
          
            file.CopyTo(fs);
            return FileName;
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName,fileName);

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
      
    }
}
