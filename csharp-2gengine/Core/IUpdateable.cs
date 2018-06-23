using System;

namespace Core
{
    public interface IUpdateable
    {
        Boolean IsEnabled { get; }
        void Update();
    }
}
