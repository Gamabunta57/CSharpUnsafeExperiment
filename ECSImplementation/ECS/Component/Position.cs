using ECSFoundation.MemoryManagement.Attributes;
using Microsoft.Xna.Framework;

namespace ECSImplementation.ECS.Component
{
    [AllocateMemory(65535)]
    public struct Position
    {
        public Vector2 Value;
    }
}
