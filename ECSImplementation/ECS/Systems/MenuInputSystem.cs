using ECSFoundation.ECS.Systems;
using ECSImplementation.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ECSImplementation.ECS.Systems
{
    class MenuInputSystem : ISystem
    {
        public MenuInputSystem(IMenu menu)
        {
            _priorState = Keyboard.GetState();
            _menu = menu;
        }

        public void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Up) && _priorState.IsKeyUp(Keys.Up))
                _menu.SelectPriorItem();

            else if (state.IsKeyDown(Keys.Down) && _priorState.IsKeyUp(Keys.Down))
                _menu.SelectNextItem();

            else if (state.IsKeyDown(Keys.Enter) && _priorState.IsKeyUp(Keys.Enter))
                _menu.ConfirmItem();

            _priorState = state;
        }

        readonly IMenu _menu;
        KeyboardState _priorState;
    }
}
