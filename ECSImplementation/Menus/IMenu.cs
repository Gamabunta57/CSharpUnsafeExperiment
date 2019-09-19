using System;
using Microsoft.Xna.Framework;

namespace ECSImplementation.Menus
{
    public interface IMenu
    {
        uint CurrentSelected { get; }
        Tuple<string, Action>[] Items { get; }
        Vector2 Position { get; }

        void SelectPriorItem();
        void SelectNextItem();

        void ConfirmItem();
    }
}
