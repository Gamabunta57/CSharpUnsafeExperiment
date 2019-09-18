using System;
using System.Collections.Generic;
using System.Text;

namespace ECSImplementation.Menus
{
    public interface IMenu
    {
        uint CurrentSelected { get; }
        Tuple<string, Action>[] Items { get; }

        void SelectPriorItem();
        void SelectNextItem();

        void ConfirmItem();
    }
}
