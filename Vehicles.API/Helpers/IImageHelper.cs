using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Vehicles.API.Helpers
{
    public interface IImageHelper
    {
        //Task<string> UpLoadImageAsync(IFormFile formFile, string folder);
        Task<string> UpLoadImageAsync(IFormFile formFile, string folder); //IFormFile seleccionamos desde la interface del usuario

        //string UpLoadImage(byte[] pictureArray, string folder);
        Task<Guid> UpLoadImageAsync(byte[] pictureArray, string folder); //byte[] para subie desde el móvil

        Task<Guid> UploadBlobAsync(string image, string containerName); //string image pasa una ruta

        Task DeleteBlobAsync(Guid id, string containerName); //Guid id permite pasar el id del blob
    }
}
