using ECSFoundation.ECS.Systems;
using ECSImplementation.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ECSImplementation.ECS.Systems
{
    class ProcessInputSystem : ISystem
    {
        IMovable _player1;
        IMovable _player2;

        public void Init(IMovable player1, IMovable player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        public void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            var gamePadState = GamePad.GetState(ControllerState.Player1Index);

            _player1.Heading.Value.Y = state.IsKeyDown(Keys.Z) || gamePadState.IsButtonDown(Buttons.LeftThumbstickUp) ?
                                       -1 : 
                                       state.IsKeyDown(Keys.S) || gamePadState.IsButtonDown(Buttons.LeftThumbstickDown) ? 
                                       1 :
                                       0;

            _player2.Heading.Value.Y = state.IsKeyDown(Keys.Up) || gamePadState.IsButtonDown(Buttons.RightThumbstickUp) ?
                                       -1 : 
                                       state.IsKeyDown(Keys.Down) || gamePadState.IsButtonDown(Buttons.RightThumbstickDown)  ? 
                                       1 :
                                       0;

            if (state.IsKeyDown(Keys.Escape)  || gamePadState.IsButtonDown(Buttons.Start))
                MatchState.AskForPaused = true;
        }
    }
}
