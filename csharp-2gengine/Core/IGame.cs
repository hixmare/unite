namespace Core
{
    public interface IGame
    {
        TComponent CreateComponent<TData, TComponent>(TData entity)
            where TData : IComponentDataSource
            where TComponent : IGameComponent<TData>;
        void UpdateAllComponents();
        void DrawAllComponents();
    }
}