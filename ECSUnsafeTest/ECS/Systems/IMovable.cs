using ECSFoundation.ECS.Entities;
using ECSUnsafeTest.ECS.Component;

namespace ECSUnsafeTest.Systems
{
    public interface IMovable : IEntity
    {
        ref Position Position { get; }
        ref Heading Heading { get; }
    }
}
