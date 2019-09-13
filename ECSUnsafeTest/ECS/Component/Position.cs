using ECSUnsafeTest.MemoryManagement.Attributes;
using ECSUnsafeTest.utils;

namespace ECSUnsafeTest.ECS.Component
{
    [AllocateMemory(0x03)]
    public struct Position
    {
        public Vector2 Value;
    }
}
