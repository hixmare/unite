using System;

namespace Core
{
    public interface IDrawable
    {
        Boolean IsVisible { get; }
        void Draw();
    }
}
