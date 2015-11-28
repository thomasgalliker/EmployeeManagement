
namespace Employee.Mapping.Abstraction
{
    public interface IMapper<in TSource, out TDest>
    {
        TDest Map(TSource src);
    }
}
