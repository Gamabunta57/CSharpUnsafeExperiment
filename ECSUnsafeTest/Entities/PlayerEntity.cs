using ECSUnsafeTest.Attributes;
using ECSUnsafeTest.core;

namespace ECSUnsafeTest.Entities
{
    [AllocateMemory(0x02)]
    public struct PlayerEntity
    {
        public BaseEntity baseEntity;
        public Vector2 position;
        public Vector2 heading;
        public RectCollider collider;
    }
}
