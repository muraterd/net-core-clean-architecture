using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Utils.Extensions
{
    public static class ImageSharpExtensions
    {
        public static Image CropSquareCentered(this Image image)
        {
            var size = Math.Min(image.Width, image.Height);

            // Calculate crop area
            var rectX = ((image.Width - size) / 2);
            var rectY = ((image.Height - size) / 2);
            var cropArea = new Rectangle(rectX, rectY, size, size);

            // Crop the image
            image.Mutate(i => i.Crop(cropArea));

            return image;
        }
    }
}
