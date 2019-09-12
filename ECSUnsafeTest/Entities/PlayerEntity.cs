using ECSUnsafeTest.Component;

namespace ECSUnsafeTest.Entities
{
    unsafe public struct PlayerEntity : IEntity
    {
        public BaseEntity BaseEntity { get => *baseEntity; set => *baseEntity = value; }
        public Position Position { get => *position; set => *position = value; }
        public Heading Heading { get => *heading; set => *heading = value; }
        public RectCollider Collider { get => *collider; set => *collider = value; }

        internal BaseEntity* baseEntity;
        internal Position* position;
        internal Heading* heading;
        internal RectCollider* collider;
    }
}
