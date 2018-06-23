namespace Core
{
    public interface IGameComponent<T> : IUpdateable, IDrawable 
        where T : IComponentDataSource
    {
        IGame Game { get; }
        
    }
}