using Microsoft.Extensions.FileProviders;

namespace THNETII.Docgen.ViewComponents
{
    public static class LibraryEmbedded
    {
        internal const string GuidStringConst = "ACD8F964-8414-40DC-AAE1-8F93165EB0F8";

        public static string GuidString { get; } = GuidStringConst;

        public static EmbeddedFileProvider FileProvider { get; } =
            new EmbeddedFileProvider(typeof(LibraryEmbedded).Assembly);
    }
}
