using QRCoder;
using SixLabors.ImageSharp;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Color = System.Drawing.Color;
using Image = SixLabors.ImageSharp.Image;

namespace QRCoderSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var url = args[0];
            var fileName = args[1];

            Console.WriteLine("Url: " + url);
            Console.WriteLine("FileName: " + fileName + ".png");

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                var iconAsImage = Bitmap.FromFile("moriyama-square.png");
                var iconAsSixLaborsImage = iconAsImage.ToImageSharpImage();
                var iconAsBitmap = iconAsSixLaborsImage.ToBitmap();
                Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, icon: iconAsBitmap);
                SixLabors.ImageSharp.Image imageToSave = qrCodeImage.ToImageSharpImage();

                ImageConverter converter = new ImageConverter();
                byte[] qrImageBytes = (byte[])converter.ConvertTo(qrCodeImage, typeof(byte[]));

                System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(qrImageBytes));
                image.Save(fileName + ".png", ImageFormat.Png);
            }
        }
    }
}
