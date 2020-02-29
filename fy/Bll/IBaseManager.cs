namespace Bll
{
    public interface IBaseManager<T> where T : class
    {
        int AddByDto<TSource>(TSource dto) where TSource : class;
        T Create();
        T CreateByDto<TSource>(TSource dto) where TSource : class;
    }
}