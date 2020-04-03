using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Resources;

using Microsoft.Extensions.Localization;

namespace THNETII.Docgen.ViewComponents.Internal
{
    public static class StringLocalizerExtensions
    {
        private static readonly ConcurrentDictionary<Type, ResourceManagerStringLocalizer> fallbackLocalizers =
            new ConcurrentDictionary<Type, ResourceManagerStringLocalizer>();

        private static readonly Func<Type, ResourceManagerStringLocalizer> CreateFallbackLocalizer = t =>
        {
            var cache = new ResourceNamesCache();
            var asm = typeof(StringLocalizerExtensions).Assembly;
            var resLoc = asm.GetCustomAttribute<ResourceLocationAttribute>()
                ?.ResourceLocation ?? string.Empty;
            var segments = new string[]
            {
                asm.GetName().Name,
                resLoc,
                t.FullName
            };
            var baseName = string.Join(".", segments.Where(s => !string.IsNullOrWhiteSpace(s)));

            var resMgr = new ResourceManager(baseName, asm);
            return new ResourceManagerStringLocalizer(resMgr, asm, baseName, cache,
                Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance);
        };

        public static LocalizedString GetStringOrFallback<T>(
            this IStringLocalizer<T> localizer, string name)
        {
            if (localizer?[name] is LocalizedString localized &&
                localized.ResourceNotFound is false)
                return localized;
            var fallback = fallbackLocalizers
                .GetOrAdd(typeof(T), CreateFallbackLocalizer);
            return fallback[name];
        }

        public static LocalizedString GetStringOrFallback<T>(
            this IStringLocalizer<T> localizer, string name,
            params object[] arguments)
        {
            if (localizer?[name, arguments] is LocalizedString localized &&
                localized.ResourceNotFound is false)
                return localized;
            var fallback = fallbackLocalizers
                .GetOrAdd(typeof(T), CreateFallbackLocalizer);
            return fallback[name, arguments];
        }
    }
}
