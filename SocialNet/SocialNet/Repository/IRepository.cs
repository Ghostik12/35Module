namespace SocialNet.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Update(T item);
        void Delete(int id);
        void Create(T item);
    }
}
