using ECSFoundation.ECS.Component;
using ECSUnsafeTest.ECS.Component;
using ECSUnsafeTest.ECS.Systems;

namespace ECSUnsafeTest.ECS.Entities
{
    public unsafe struct BallEntity : IMovable, ICollidable
    {
        public ref BaseEntity BaseEntity => ref *baseEntity;
        public ref Position Position => ref *position;
        public ref Heading Heading => ref *heading;
        public ref RectCollider Collider => ref *collider;

        internal BaseEntity* baseEntity;
        internal Position* position;
        internal Heading* heading;
        internal RectCollider* collider;
    }
}
