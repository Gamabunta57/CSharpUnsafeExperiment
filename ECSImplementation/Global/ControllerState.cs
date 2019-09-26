namespace ECSImplementation.Global
{
    public static class ControllerState
    {
        public static byte Player1Index = 255;
        public static byte Player2Index = 255;

        public static ControllerType PressAnyKeyResultType;
    }

    public enum ControllerType
    {
        Undefined,
        GamePad,
        Keyboard
    }


}
