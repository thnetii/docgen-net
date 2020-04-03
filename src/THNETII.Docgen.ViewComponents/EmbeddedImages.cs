using Microsoft.Extensions.FileProviders;

namespace THNETII.Docgen.ViewComponents
{
    public static class EmbeddedImages
    {
        public static IFileProvider FileProvider { get; } =
            new EmbeddedFileProvider(
                typeof(EmbeddedImages).Assembly,
                typeof(EmbeddedImages).Assembly.GetName().Name + ".Images"
                );
    }
}
