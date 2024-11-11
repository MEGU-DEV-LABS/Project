using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;

namespace DbWebApplication.Services
{
    public class QrCodeService
    {
        public QrCodeService()
        {
        }

        public async Task<string> ReadQRCode(byte[] byteArray)
        {
            Bitmap target;
            target = ByteToBitmap(byteArray);
            var source = new BitmapLuminanceSource(target);
            var bitmap = new BinaryBitmap(new HybridBinarizer(source));
            var reader = new QRCodeReader();
            var result = reader.decode(bitmap);

            if (result == null)
            {
                target = PreprocessImage(target);

                var reader2 = new BarcodeReader
                {
                    AutoRotate = true, // Автоматичне обертання для покращення розпізнавання
                    TryInverted = true // Спроба розпізнати інвертований код
                };

                var result2 = reader2.Decode(target);
                return result2.Text;
            }
            
            /*Bitmap bitmap = ByteToBitmap(byteArray);
            bitmap = PreprocessImage(bitmap);

            var reader = new BarcodeReader
            {
                AutoRotate = true, // Автоматичне обертання для покращення розпізнавання
                TryInverted = true // Спроба розпізнати інвертований код
            };

            var result = reader.Decode(bitmap);*/
            return result.Text;
            
            /*Bitmap bitmap;
            using (var stream = imageFile.OpenReadStream())
            {
                bitmap = new Bitmap(stream);
            }

            bitmap = PreprocessImage(bitmap);
            // Створення екземпляру BarcodeReader для зчитування QR-коду
            var barcodeReader = new BarcodeReader
            {
                AutoRotate = true,    // Автоматичне обертання зображення
                TryInverted = true    // Спроба розпізнати інвертований код
            };

            // Розшифрування зображення
            var result = barcodeReader.Decode(bitmap);
            return result.Text;*/
        }

        private static Bitmap ByteToBitmap(byte[] byteArray) 
        {
            Bitmap target;
            using (var stream = new MemoryStream(byteArray)) {
                target = new Bitmap(stream);
            }

            return target;
        }
        
        private Bitmap PreprocessImage(Bitmap bitmap)
        {
            // Налаштування розміру зображення для покращення якості розпізнавання
            var resized = new Bitmap(bitmap, new Size(bitmap.Width * 2, bitmap.Height * 2));
    
            // Перетворення на чорно-біле зображення
            for (int y = 0; y < resized.Height; y++)
            {
                for (int x = 0; x < resized.Width; x++)
                {
                    var pixel = resized.GetPixel(x, y);
                    var grayScale = (int)((pixel.R * 0.3) + (pixel.G * 0.59) + (pixel.B * 0.11));
                    var color = grayScale > 128 ? Color.White : Color.Black;
                    resized.SetPixel(x, y, color);
                }
            }

            return resized;
        }
        
        public Bitmap GenerateQRCode(Guid? text)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Width = 250,
                    Height = 250,
                    Margin = 1 
                }
            };

            
            return writer.Write(text.ToString());
        }
        
        public byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                return stream.ToArray(); 
            }
        }

    }
}