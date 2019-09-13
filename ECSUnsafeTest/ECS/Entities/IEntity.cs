using ECSUnsafeTest.ECS.Component;

namespace ECSUnsafeTest.Entities
{
    public interface IEntity
    {
        ref BaseEntity BaseEntity { get; }
    }
}
