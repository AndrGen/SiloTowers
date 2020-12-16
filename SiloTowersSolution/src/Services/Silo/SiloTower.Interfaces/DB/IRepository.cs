namespace B2BSales.Interfaces.DB
{
    public interface IRepository<T>: IReadOnlyRepository<T> where T : class
    {

        void Add(T entity);
        void Delete(T entityToDelete);
        void Delete(object id);
        void Edit(T entity);
    }
}
