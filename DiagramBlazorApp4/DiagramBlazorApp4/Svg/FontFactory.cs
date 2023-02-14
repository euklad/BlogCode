using SkiaSharp;
using System.Reflection;

namespace DiagramBlazorApp4.Svg
{
    public static class FontFactory
    {
        public static SKTypeface Georgia => Load(@"Georgia.ttf");
        public static SKTypeface Roboto => Load(@"Roboto-Regular.ttf");
        public static SKTypeface Verdana => Load(@"Verdana.ttf");

        private static SKTypeface Load(string fileName)
        {
            var path = Path.Combine(AssemblyDirectory, fileName);
            var font = SKTypeface.FromFile(path);

            if (font == null)
            {
                throw new Exception($"Cannot load font {fileName}");
            }

            return font;
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
