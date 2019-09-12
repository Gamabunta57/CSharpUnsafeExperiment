using ECSUnsafeTest.Attributes;
using ECSUnsafeTest.core;

namespace ECSUnsafeTest.Component
{
    [AllocateMemory(0x02)]
    public struct Heading
    {
        public Vector2 Value;
    }
}
