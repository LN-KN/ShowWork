using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
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
        public string GetImageFileName(string fileName)
        {
            string dir = "/images/" + GetWebFileFolder(fileName);
            CreateFolder(FOLDER_PREFIX + dir);
            return dir + "/" + Path.GetFileNameWithoutExtension(fileName) + ".jpg";
        }

        public string GetFileName(string fileName)
        {
            string dir = "/files/" + GetWebFileFolder(fileName);
            CreateFolder(FOLDER_PREFIX + dir);
            return dir + "/" + Path.GetFileName(fileName);
        }

        public string GetWebFileFolder(string fileName)
        {
            MD5 md5Hash = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(fileName);
            byte[] hashBytes = md5Hash.ComputeHash(inputBytes);

            string hash = Convert.ToHexString(hashBytes);

            return hash.Substring(0, 2) + "/" + hash.Substring(0, 4);
        }

        public void CreateFolder(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public async Task UploadAndResizeImageProfile(Stream stream, string fileName, int newWidth, int newHeight)
        {
            using (Image image = await Image.LoadAsync(stream))
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
                await image.SaveAsJpegAsync(FOLDER_PREFIX + fileName, new JpegEncoder() { Quality = 75 });
            }
        }

        public async Task UploadAndResizeImageWork(byte[] buffer, string fileName, int newWidth, int newHeight)
        {
            using (Image image = Image.Load(buffer))
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
                await image.SaveAsJpegAsync(FOLDER_PREFIX + fileName, new JpegEncoder() { Quality = 75 });
            }
        }
        public void UploadFile(Stream stream, string fileName)
        {
            using (Stream filestream = File.Create(FOLDER_PREFIX + fileName))
            {
                CopyStream(stream, filestream);
            }
        }
    
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }


    }
}
