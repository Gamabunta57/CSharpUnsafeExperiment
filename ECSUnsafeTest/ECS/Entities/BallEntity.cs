using ECSFoundation.ECS.Component;
using ECSUnsafeTest.ECS.Component;
using ECSUnsafeTest.Systems;

namespace ECSUnsafeTest.Entities
{
    public unsafe struct BallEntity : IMovable
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
