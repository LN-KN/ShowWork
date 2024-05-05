    using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Security.Cryptography;
using System.Text;

namespace ShowWork.Service
{
    public class WebFile
    {
        const string FOLDER_PREFIX = "./wwwroot";
        public WebFile()
        {
        }
        public string GetFileName(string fileName)
        {
            string dir = GetWebFileFolder(fileName);
            CreateFolder(FOLDER_PREFIX + dir);
            return dir + "/" + Path.GetFileNameWithoutExtension(fileName) + ".jpg";
        }
        public string GetWebFileFolder(string fileName)
        {
            MD5 md5Hash = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(fileName);
            byte[] hashBytes = md5Hash.ComputeHash(inputBytes);

            string hash = Convert.ToHexString(hashBytes);

            return "/images/" + hash.Substring(0, 2) + "/" + hash.Substring(0, 4);
        }

        public void CreateFolder(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public async Task UploadAndResizeImage(Stream fileStream, string fileName, int newWidth, int newHeight)
        {
            using(Image image = await Image.LoadAsync(fileStream))
            {
                int aspectWidth = newWidth;
                int aspectHeight = newHeight;
                ResizeOptions resizeOptions = new ResizeOptions()
                {
                    Mode = ResizeMode.Max,
                    Sampler = KnownResamplers.Lanczos3,
                    Size = new Size(newWidth, newHeight)
                };
                image.Mutate(x => x.Resize(resizeOptions));
                await image.SaveAsJpegAsync(FOLDER_PREFIX + fileName, new JpegEncoder() {Quality = 75});
            }
        }
    }
}
