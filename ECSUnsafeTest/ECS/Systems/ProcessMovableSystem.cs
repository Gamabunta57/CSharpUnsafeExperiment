using System;
using System.Collections.Generic;
using ECSUnsafeTest.Entities;
using ECSUnsafeTest.MemoryManagement.MemoryAllocators;

namespace ECSUnsafeTest.Systems
{
    public class ProcessMovableSystem : ISystem
    {

        public ProcessMovableSystem(MemoryAllocator allocator){
            memory = allocator;
        }

        public void OnRegisterEntity(IEntity entity)
        {
            if(entity is IMovable movable)
                idList.Add(movable);
        }

        public void Update()
        {
            foreach(var entity in idList)
            {
                Console.WriteLine($"Entity#{entity.BaseEntity.Id} before: {entity.Position.Value}");

                entity.Position.Value += entity.Heading.Value;

                Console.WriteLine($"Entity#{entity.BaseEntity.Id} after:  {entity.Position.Value}");
            }
        }

        readonly MemoryAllocator memory;
        readonly IList<IMovable> idList = new List<IMovable>();
    }
}
