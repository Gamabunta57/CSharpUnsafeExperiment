using ECSFoundation.ECS.Systems;
using ECSImplementation.Global;
using ECSImplementation.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ECSImplementation.ECS.Systems
{
    class MenuInputSystem : ISystem
    {
        public MenuInputSystem(IMenu menu)
        {
            _priorKeyboardState = Keyboard.GetState();
            _priorGamePadState = GamePad.GetState(ControllerState.Player1Index);
            _menu = menu;
        }

        public void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            var gamePadState = GamePad.GetState(ControllerState.Player1Index);

            if (state.IsKeyDown(Keys.Up) && _priorKeyboardState.IsKeyUp(Keys.Up) ||
                gamePadState.IsButtonDown(Buttons.DPadUp) && _priorGamePadState.IsButtonUp(Buttons.DPadUp) ||
                gamePadState.ThumbSticks.Left.Y > 0.5 && _priorGamePadState.ThumbSticks.Left.Y <= 0.5
            )
                _menu.SelectPriorItem();

            else if (state.IsKeyDown(Keys.Down) && _priorKeyboardState.IsKeyUp(Keys.Down) ||
                gamePadState.IsButtonDown(Buttons.DPadDown) && _priorGamePadState.IsButtonUp(Buttons.DPadDown) ||
                gamePadState.ThumbSticks.Left.Y < -0.5 && _priorGamePadState.ThumbSticks.Left.Y >= -0.5
                )
                _menu.SelectNextItem();

            else if (state.IsKeyDown(Keys.Enter) && _priorKeyboardState.IsKeyUp(Keys.Enter) ||
                gamePadState.IsButtonDown(Buttons.A) && _priorGamePadState.IsButtonUp(Buttons.A)
                )
                _menu.ConfirmItem();

            _priorKeyboardState = state;
            _priorGamePadState = gamePadState;
        }

        readonly IMenu _menu;
        KeyboardState _priorKeyboardState;
        GamePadState _priorGamePadState;
    }
}
