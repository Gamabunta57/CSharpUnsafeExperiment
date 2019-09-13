using ECSFoundation.ECS.Component;

namespace ECSFoundation.ECS.Entities
{
    public interface IEntity
    {
        ref BaseEntity BaseEntity { get; }
    }
}
