using ECSFoundation.MemoryManagement.Attributes;
using Microsoft.Xna.Framework;

namespace ECSImplementation.ECS.Component
{
    [AllocateMemory(0x07)]
    public struct Position
    {
        public Vector2 Value;
    }
}
