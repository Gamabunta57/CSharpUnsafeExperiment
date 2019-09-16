using ECSFoundation.ECS.Component;
using ECSImplementation.ECS.Component;
using ECSImplementation.ECS.Systems;

namespace ECSImplementation.ECS.Entities
{
    public unsafe struct PlayerEntity : IMovable, ICollidable
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
