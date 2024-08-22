using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

public class TextureLoader
{
    public static Image<Rgba32> LoadTexture(string textureFilePath, string paletteFilePath)
    {
        using (FileStream textureStream = new FileStream(textureFilePath, FileMode.Open, FileAccess.Read))
        using (FileStream paletteStream = new FileStream(paletteFilePath, FileMode.Open, FileAccess.Read))
        using (BinaryReader textureReader = new BinaryReader(textureStream))
        using (BinaryReader paletteReader = new BinaryReader(paletteStream))
        {
            // Read width and height
            //height is byte 1, width is byte 2
            int height = textureReader.ReadInt16();
            int width = textureReader.ReadInt16();

            // Read the palette
            Rgba32[] palette = new Rgba32[256];
            for (int i = 0; i < 256; i++)
            {
                byte r = paletteReader.ReadByte();
                byte g = paletteReader.ReadByte();
                byte b = paletteReader.ReadByte();
                palette[i] = new Rgba32(r, g, b); // 24-bit color
            }

            // Read the texture data
            byte[] textureData = textureReader.ReadBytes(width * height);

            // Create a new Image<Rgba32>
            var image = new Image<Rgba32>(height, width);

            // Map the texture data to the palette and create the image, correcting the vertical flip
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = textureData[y * width + x];
                    image[y, x] = palette[index];
                }
            }

            return image;
        }
    }
}
