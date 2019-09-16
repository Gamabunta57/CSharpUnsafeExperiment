using System.Collections.Generic;
using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSUnsafeTest.ECS.Component;

namespace ECSUnsafeTest.ECS.Systems
{
    public interface IMovable : IEntity
    {
        ref Position Position { get; }
        ref Heading Heading { get; }
    }

    public class ProcessMovableSystem : ISystem
    {

        public ProcessMovableSystem() => EntityManager.OnNewEntityCreated += OnNewEntityCreated;

        public void OnNewEntityCreated(IEntity entity)
        {
            if(entity is IMovable movable)
                idList.Add(movable);
        }

        public void Update()
        {
            foreach(var entity in idList)
                entity.Position.Value += entity.Heading.Value;
        }

        readonly IList<IMovable> idList = new List<IMovable>();
    }
}
