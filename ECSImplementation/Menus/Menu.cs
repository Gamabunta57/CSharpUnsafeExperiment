using System;

namespace ECSImplementation.Menus
{
    public class Menu : IMenu
    {
        public Tuple<string, Action>[] Items { get; }
        public uint CurrentSelected { get; private set; }

        public Menu(Tuple<string, Action>[] menuItems) => Items = menuItems;

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
