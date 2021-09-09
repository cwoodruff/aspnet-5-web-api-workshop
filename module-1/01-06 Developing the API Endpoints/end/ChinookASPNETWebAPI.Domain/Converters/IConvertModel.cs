using System.Threading.Tasks;

namespace ChinookASPNETWebAPI.Domain.Converters
{
    public interface IConvertModel<TSource, TTarget>
    {
        TTarget Convert();
    }
}