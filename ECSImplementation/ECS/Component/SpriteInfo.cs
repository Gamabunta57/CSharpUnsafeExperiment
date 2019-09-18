using Microsoft.Xna.Framework;
using ECSFoundation.MemoryManagement.Attributes;

namespace ECSImplementation.ECS.Component
{
    [AllocateMemory(65535)]
    public struct SpriteInfo
    {
        public Vector2 Size;
        public Color Color;
    }
}
