using SixLabors.ImageSharp;

namespace LithRageTexUnpacker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: LithRAGETexUnpack.exe <path to texture> <path to palette>");
                return;
            }
            else
            {
                string fileName = Path.GetFileNameWithoutExtension(args[0]);
                var texture = TextureLoader.LoadTexture(args[0], args[1]);
                texture.Save($"{fileName}.png");
                return;
            }

        }
    }
}
