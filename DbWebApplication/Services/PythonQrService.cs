using Python.Runtime;

namespace DbWebApplication.Services
{
    public class PythonQrService
    {
        static PythonQrService()
        {
            // Ініціалізуємо Python Engine
            Runtime.PythonDLL = "C:\\Users\\popug\\AppData\\Local\\Programs\\Python\\Python312\\python312.dll";
            PythonEngine.Initialize();
        }

        public string DecodeQrCode(byte[] fileBytes)
        {
            if (fileBytes == null || fileBytes.Length == 0)
                throw new ArgumentException("Файл не надано або порожній.");

            try
            {
                using (Py.GIL()) // Блокуємо GIL
                {
                    // Імпортуємо необхідні модулі
                    dynamic pyzbar = Py.Import("pyzbar.pyzbar");
                    dynamic Image = Py.Import("PIL.Image");
                    dynamic io = Py.Import("io");

                    // Відкриваємо зображення з масиву байтів у Python
                    dynamic imageStream = io.BytesIO(fileBytes);
                    dynamic image = Image.open(imageStream);

                    // Декодуємо QR-код
                    dynamic qrCodeData = pyzbar.decode(image);

                    // Обробляємо результат (припускаємо, що на зображенні один QR-код)
                    foreach (var qr in qrCodeData)
                    {
                        // Повертаємо дані QR-коду у вигляді рядка
                        return qr.data.ToString();
                    }
                }

                // Якщо QR-код не знайдено, повертаємо повідомлення про це
                return "QR-код не знайдено.";
            }
            catch (PythonException ex)
            {
                // Логування помилки Python
                Console.WriteLine("Python Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Логування загальних помилок
                Console.WriteLine("General Exception: " + ex.Message);
            }

            // Повертаємо null, якщо виникла помилка або QR-код не знайдено
            return null;
        }
    }
}
