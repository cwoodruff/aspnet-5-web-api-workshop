using System.Collections.Generic;
using System.Linq;
using ChinookASPNETWebAPI.Domain.Converters;

namespace ChinookASPNETWebAPI.Domain.Extensions
{
    public static class ConvertExtensions
    {
        public static IEnumerable<TTarget> ConvertAll<TSource, TTarget>(
            this IEnumerable<IConvertModel<TSource, TTarget>> values)
            => values.Select(value => value.Convert());
    }
}