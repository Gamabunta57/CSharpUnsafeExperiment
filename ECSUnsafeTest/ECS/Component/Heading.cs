using ECSUnsafeTest.MemoryManagement.Attributes;
using ECSUnsafeTest.core;

namespace ECSUnsafeTest.ECS.Component
{
    [AllocateMemory(0x03)]
    public struct Heading
    {
        public Vector2 Value;
    }
}
