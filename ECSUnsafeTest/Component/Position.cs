using ECSUnsafeTest.Attributes;
using ECSUnsafeTest.core;

namespace ECSUnsafeTest.Component
{
    [AllocateMemory(0xFFFF)]
    public struct Position
    {
        public Vector2 value;
        public Vector2 value2;
        public Vector2 value3;
        public Vector2 value4;
        public Vector2 value5;
        public Vector2 value6;
        public Vector2 value7;
    }
}
