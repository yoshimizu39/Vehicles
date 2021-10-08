using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles.API.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public Task DeleteBlobAsync(Guid id, string containerName)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UploadBlobAsync(string image, string containerName)
        {
            throw new NotImplementedException();
        }

        //public async Task<Guid> UpLoadImageAsync(IFormFile formFile, string folder)
        //{
        //    //Stream stream = formFile.OpenReadStream(); //creamos un stream
        //    Guid guid = Guid.NewGuid();
        //    //Guid guid = Guid.NewGuid(); //generamos el nombre del archivo
        //    //string file = $"{guid}.jpg";
        //    //string file = $"{guid}";
        //    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{folder}", $"{guid}");

        //    using (FileStream stream = new FileStream(path, FileMode.Create))
        //    {
        //        await formFile.CopyToAsync(stream);
        //    }

        //    return Guid.Parse($"~/images/{folder}/{guid}");
        //}

        public Task<Guid> UpLoadImageAsync(byte[] pictureArray, string folder)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpLoadImageAsync(IFormFile imageFile, string folder)
        {
            string guid = Guid.NewGuid().ToString();
            string file = $"{guid}.jpg";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{folder}", file);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"~/images/{folder}/{file}";
        }
    }
}
