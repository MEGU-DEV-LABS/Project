using System.Drawing;

namespace DbWebApplication.Interfaces.IServices;

public interface IQrCodeService
{
    Task<string> ReadQRCode(byte[] byteArray);
    Bitmap GenerateQRCode(Guid? text);
    byte[] ConvertBitmapToByteArray(Bitmap bitmap);
    Task<byte[]> ConvertImageToByteArrayAsync(IFormFile imageFile);
}