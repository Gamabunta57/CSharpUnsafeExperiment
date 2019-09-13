using ECSUnsafeTest.ECS.Component;
using ECSUnsafeTest.Entities;

namespace ECSUnsafeTest.Systems
{
    public interface IMovable : IEntity
    {
        ref Position Position { get; }
        ref Heading Heading { get; }
    }
}
