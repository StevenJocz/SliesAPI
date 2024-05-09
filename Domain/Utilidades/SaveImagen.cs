using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Utilidades
{
    public interface ISaveImagen
    {
        Task<string> SaveImageAsync(string base64Image, string ruta);
        Task<bool> DeleteImage(string imageUrl);
    }
    public class SaveImagen : ISaveImagen
    {
        private string rutaUrl = "http://localhost:5239/";
        //private string rutaUrl = "https://antopia.site/";

        public async Task<string> SaveImageAsync(string base64Image, string ruta)
        {
            try
            {
                string[] base64Parts = base64Image.Split(',');

                if (base64Parts.Length != 2)
                {
                    throw new ArgumentException("La cadena Base64 no tiene el formato esperado.");
                }

                // La segunda parte contiene la representación Base64 de la imagen
                string base64Data = base64Parts[1];
                byte[] imageBytes = Convert.FromBase64String(base64Data);
                string fileName = $"{Guid.NewGuid()}.jpg";
                string filePath = Path.Combine(ruta, fileName);
                await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(base64Data));
                string[] rutaDos = ruta.Split('/');
                string rutaImagen = rutaUrl + rutaDos[1] + "/" + fileName;

                return rutaDos[1] + "/" + fileName;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public async Task<bool> DeleteImage(string imageUrl)
        {
            try
            {
                var fullPath = Path.Combine("wwwroot", imageUrl);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
