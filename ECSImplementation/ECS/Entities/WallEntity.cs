using ECSFoundation.ECS.Component;
using ECSImplementation.ECS.Component;
using ECSImplementation.ECS.Systems;
using ECSImplementation.ECS.Systems.DrawSystem;

namespace ECSImplementation.ECS.Entities
{
    public unsafe struct WallEntity : ICollidable, IDrawable
    {
        public ref BaseEntity BaseEntity => ref *baseEntity;
        public ref Position Position => ref *position;
        public ref RectCollider Collider => ref *collider;
        public ref SpriteInfo SpriteInfo => ref *spriteInfo;

        internal BaseEntity* baseEntity;
        internal Position* position;
        internal RectCollider* collider;
        internal SpriteInfo* spriteInfo;
    }
}
