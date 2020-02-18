using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class IFormFileExtensions
    {
        public static async void Save(this IFormFile file, string path, string fileName = null)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var filePath = $"{path}/{fileName ?? (Guid.NewGuid().ToString() + fileExtension)}";
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}
