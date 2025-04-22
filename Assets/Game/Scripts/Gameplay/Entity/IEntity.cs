namespace Gameplay
{
    public interface IEntity
    {
        T Get<T>();
        public T TryGet<T>() where T : class;
    }
}