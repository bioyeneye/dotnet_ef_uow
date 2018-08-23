using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Cores;

namespace CoreLibrary.Utilities
{
    /// <summary>
    /// Upload type to directory
    /// </summary>
    public class Uploader
    {
        /// <summary>
        /// Upload image to a path
        /// </summary>
        /// <param name="model"></param> 
        /// <returns></returns>
        public static async Task<string> UploadImage(UploadModel model)
        {
            if (model.FormFile == null || model.FormFile.Length <= 0)
                return string.Empty;

            if (!Directory.Exists(model.Path))
                Directory.CreateDirectory(path: model.Path);

            string extension = Path.GetExtension(model.FormFile.FileName);
            if (!IsImage(extension))
            {
                return string.Empty;
            }

            string filename = DateTime.Now.ToString("ddMMyyyyhhmmss") + model.FormFile.FileName;
            using (var stream = new FileStream(model.Path, FileMode.Create))
            {
                await model.FormFile.CopyToAsync(stream);
            }

            if (model.DeleteOldFile)
            {
                string oldFilePath = Path.Combine(model.Path, model.OldFile);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }

            return filename;
        }

        private static bool IsImage(string extension)
        {
            return new[] {"jpg", "jpeg", "png", "gif"}.Contains(extension.ToLower());
        }
    }
}