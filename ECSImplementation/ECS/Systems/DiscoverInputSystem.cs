using ECSFoundation.ECS.Systems;
using ECSImplementation.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace ECSImplementation.ECS.Systems
{
    class DiscoverInputSystem : ISystem
    {
        public DiscoverInputSystem() => _emptyPadState = new GamePadState(Vector2.Zero, Vector2.Zero, 0, 0, 0);

        public void Update(GameTime gameTime)
        {
            //Debug();
            var keys = Keyboard.GetState().GetPressedKeys();

            if (keys.Length > 0) {
                ControllerState.PressAnyKeyResultType = ControllerType.Keyboard;
                return;
            }

            for(var i = 0; i < GamePad.MaximumGamePadCount; i++)
            {
                var state = GamePad.GetState(i);
                if (!state.IsConnected || _emptyPadState == state)
                    continue;

                ControllerState.Player1Index = (byte)i;
                ControllerState.PressAnyKeyResultType = ControllerType.GamePad;
            }
        }

        private void Debug()
        {
            var keys = Keyboard.GetState().GetPressedKeys();

            if (keys.Length > 0)
            {
                for (var i = 0; i < keys.Length; i++)
                    Console.Write($"{keys[i]} ");

                Console.WriteLine("");
            }

            var enumSize = Enum.GetNames(typeof(PlayerIndex)).Length;
            for (var i = 0; i < enumSize; i++)
            {
                var isConnected = GamePad.GetState(i).IsConnected;
                Console.Write($"P {i + 1} is connected: {isConnected}");
            }

            Console.WriteLine("");

            for (var i = 0; i < enumSize; i++)
            {
                var isConnected = GamePad.GetState(i).IsConnected;

                if (isConnected)
                {
                    Console.WriteLine(GamePad.GetState(i).Buttons.ToString());
                    Console.WriteLine(GamePad.GetState(i).DPad.ToString());
                    Console.WriteLine(GamePad.GetState(i).ThumbSticks.ToString());
                    Console.WriteLine(GamePad.GetState(i).Triggers.ToString());
                }
            }
        }

        readonly GamePadState _emptyPadState;
    }
}
