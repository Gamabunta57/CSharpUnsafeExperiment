using System;
using Microsoft.Xna.Framework;

namespace ECSImplementation.Menus
{
    public class Menu : IMenu
    {
        public Tuple<string, Action>[] Items { get; }
        public Vector2 Position { get; private set; }
        public uint CurrentSelected { get; private set; }

        public Menu(Tuple<string, Action>[] menuItems, Vector2 position)
        {
            Items = menuItems;
            Position = position;
        }

        public void SelectPriorItem()
        {
            if (CurrentSelected > 0)
                CurrentSelected--;
        }

        public void SelectNextItem()
        {
            if (CurrentSelected < Items.Length - 1)
                CurrentSelected++;
        }

        public void ConfirmItem() => Items[CurrentSelected].Item2.Invoke();
    }
}
