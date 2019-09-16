using ECSFoundation.ECS.Component;
using ECSImplementation.ECS.Component;
using ECSImplementation.ECS.Systems;

namespace ECSImplementation.ECS.Entities
{
    public unsafe struct WallEntity : ICollidable
    {
        public ref BaseEntity BaseEntity => ref *baseEntity;
        public ref Position Position => ref *position;
        public ref RectCollider Collider => ref *collider;

        internal BaseEntity* baseEntity;
        internal Position* position;
        internal RectCollider* collider;
    }
}
