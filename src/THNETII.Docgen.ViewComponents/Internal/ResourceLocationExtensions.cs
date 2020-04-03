using System.Reflection;

using Microsoft.Extensions.Localization;

namespace THNETII.Docgen.ViewComponents.Internal
{
    public static class ResourceLocationExtensions
    {
        public static string ToLocalizedString(
            this ResourceLocation value,
            IStringLocalizer<ResourceLocation> localizer) =>
            FlagsExtensions.ToLocalizedString(value, localizer,
                (l, r) => l & r, (l, r) => l & ~r);
    }
}
