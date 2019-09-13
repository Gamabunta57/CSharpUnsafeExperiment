using System;
using System.Collections.Generic;
using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;

namespace ECSUnsafeTest.Systems
{
    public class ProcessMovableSystem : ISystem
    {
    
        public ProcessMovableSystem()
        {
            EntityManager.OnNewEntityCreated += OnNewEntityCreated;
        }

        public void OnNewEntityCreated(IEntity entity)
        {
            if(entity is IMovable movable)
                idList.Add(movable);
        }

        public void Update()
        {
            foreach(var entity in idList)
            {
                Console.WriteLine($"Entity#{entity.BaseEntity.Id} before: {entity.Position.Value}, heading :{entity.Heading.Value}");

                entity.Position.Value += entity.Heading.Value;

                Console.WriteLine($"Entity#{entity.BaseEntity.Id} after:  {entity.Position.Value}, heading :{entity.Heading.Value}");
            }
        }

        readonly IList<IMovable> idList = new List<IMovable>();
    }
}
