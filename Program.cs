using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueShifter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // import all images into a list of images
            var images = new List<Image>();

            // get all files in the current directory
            var files = Directory.GetFiles(Directory.GetCurrentDirectory());

            // filter out all non-image files
            var imageFiles = files.Where(f => f.EndsWith(".jpg") || f.EndsWith(".png"));

            // load all images into the list
            foreach (var file in imageFiles)
            {
                images.Add(Image.FromFile(file));

                // create a new image with the same size as the first image
                var newImage = new Bitmap(images[0].Width, images[0].Height);

                // create a graphics object to draw on the new image
                var graphics = Graphics.FromImage(newImage);

                // loop through all pixels in the image
                for (int x = 0; x < newImage.Width; x++)
                {
                    for (int y = 0; y < newImage.Height; y++)
                    {
                        // read the color of the pixel from the first image and hue shift it by 180 degrees then write it to the new image

                        // convert image to bitmap
                        var bitmap = (Bitmap)images.Last();

                        var color = bitmap.GetPixel(x, y);
                        var newColor = Color.FromArgb(color.A, (color.R + 90) % 255, (color.G + 90) % 255, (color.B + 90) % 255);
                        newImage.SetPixel(x, y, newColor);

                        // draw the new image on the graphics object
                        graphics.DrawImage(newImage, 0, 0);
                    }
                }

                // save the new image with a random name
                newImage.Save(Path.GetRandomFileName() + ".png");
            }
        }
    }
}
