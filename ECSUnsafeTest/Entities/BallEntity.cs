using ECSUnsafeTest.Attributes;
using ECSUnsafeTest.core;

namespace ECSUnsafeTest.Entities
{
    [AllocateMemory(0x1)]
    public struct BallEntity
    {
        public BaseEntity baseEntity;
        public Vector2 position;
        public Vector2 heading;
        public RectCollider collider;
    }
}
