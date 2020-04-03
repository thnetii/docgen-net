using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Localization;

namespace THNETII.Docgen.ViewComponents.Internal
{
    public static class FlagsExtensions
    {
        private static readonly ConcurrentDictionary<Type, Array> _knownValues =
            new ConcurrentDictionary<Type, Array>();

        private static readonly Func<Type, Array> GetKnownValues =
            t => Enum.GetValues(t);

        public static string ToLocalizedString<T>(T value,
            IStringLocalizer<T> localizer,
            Func<T, T, T> orFunc, Func<T, T, T> andNotFunc)
            where T : struct, Enum
        {
            var knownValues = (T[])_knownValues.GetOrAdd(typeof(T), GetKnownValues);

            StringBuilder sb = new StringBuilder();
            T all = default;
            foreach (T resLocFlag in knownValues)
            {
                if (value.HasFlag(resLocFlag))
                {
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(localizer.GetStringOrFallback(resLocFlag.ToString()));
                }

                all = orFunc(all, resLocFlag);
            }

            T remainder = andNotFunc(value, all);
            if (EqualityComparer<T>.Default.Equals(remainder, default))
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(remainder.ToString());
            }

            return sb.ToString();
        }
    }
}
