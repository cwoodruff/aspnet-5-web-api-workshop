using System.Threading.Tasks;

namespace Chinook.Domain.Converters
{
    public interface IConvertModel<TSource, TTarget>
    {
        TTarget Convert();
        Task<TTarget> ConvertAsync();
    }
}